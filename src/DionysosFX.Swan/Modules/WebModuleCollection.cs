using DionysosFX.Swan.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DionysosFX.Swan.Modules
{
    public class WebModuleCollection : IWebModuleCollection
    {
        private List<WebModuleBase> _modules = null;
        private List<(string, WebModuleBase)> _modulesWithNames = null;

        public WebModuleCollection()
        {
            _modules = new List<WebModuleBase>();
            _modulesWithNames = new List<(string, WebModuleBase)>();
        }

        public void Add(string name, WebModuleBase module)
        {
            if (_modulesWithNames.Any(f => f.Item1 == name))
                throw new Exception($"{nameof(module)} Already added.");

            _modules.Add(module);
            _modulesWithNames.Add((name, module));
        }      

        public void Start(string name,CancellationToken cancellationToken)
        {
            var module = _modulesWithNames.FirstOrDefault(f => f.Item1 == name);
            if (module.Item2 == null)
                throw new Exception($"{nameof(module)} Not Found!");

            module.Item2.Start(cancellationToken);
        }

        public void Start(CancellationToken cancellationToken)
        {
            foreach (IWebModule module in _modules)
                module.Start(cancellationToken);
        }

        public void Dispose()
        {
            foreach (var module in _modules)
            {
                module.Dispose();            
            }

            _modules.Clear();
            _modulesWithNames.Clear();
        }

        public async Task DispatchRequestAsync(IHttpContext context)
        {
            if (context.IsHandled)
                return;

            foreach (var (name,module) in _modulesWithNames)
            {
                await module.HandleRequestAsync(context).ConfigureAwait(false);
                if (context.IsHandled)
                    break;
            }
        }
    }
}
