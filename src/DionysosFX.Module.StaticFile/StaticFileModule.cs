using DionysosFX.Swan.Associations;
using DionysosFX.Swan.Modules;
using DionysosFX.Swan.Net;
using DionysosFX.Swan.Threading;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace DionysosFX.Module.StaticFile
{
    public class StaticFileModule : IWebModule
    {
        ConcurrentDictionary<string, StaticFileItem> _files = null;

        public void Start(CancellationToken cancellationToken)
        {
            _files = new();
            PeriodicTask.Create(HandleExpireFiles, 1, cancellationToken);
        }

        public async Task HandleRequestAsync(IHttpContext context)
        {
            string fileName = string.Empty;
            if (context.Request.RawUrl == "/")
            {
                fileName = "index.html";
            }
            else
            {
                var entryAssembly = System.Reflection.Assembly.GetEntryAssembly();
                if (entryAssembly == null)
                    throw new Exception("Entry assembly not found");
                fileName = Path.GetDirectoryName(entryAssembly.Location);
                foreach (var dir in context.Request.RawUrl.Split('/'))
                    fileName = Path.Combine(fileName, dir);
            }

            byte[] bytes = null;
            if (_files.ContainsKey(fileName))
            {
                if (_files.TryGetValue(fileName, out StaticFileItem staticFileItem))
                {
                    bytes = staticFileItem.Data;
                }
            }
            else
            {
                try
                {
                    if (File.Exists(fileName))
                    {
                        bytes = File.ReadAllBytes(fileName);
                        _files.TryAdd(fileName, new StaticFileItem(bytes, DateTime.Now.AddMinutes(5)));
                    }
                }
                catch (Exception)
                {

                }
            }

            if (bytes != null)
            {
                context.Response.OutputStream.Write(bytes);
                context.Response.ContentType = MimeType.Associations.GetValueOrDefault(Path.GetExtension(fileName));
                context.Response.StatusCode = (int)HttpStatusCode.OK;
                context.SetHandled();
            }
        }

        private void HandleExpireFiles()
        {
            List<KeyValuePair<string, StaticFileItem>> deleteItems = new();
            foreach (var item in _files)
            {
                if (item.Value.ExpireDate < DateTime.Now)
                    deleteItems.Add(item);
            }

            foreach (var item in deleteItems)
            {
                _files.TryRemove(item);
            }

        }

        public void Dispose()
        {
            _files.Clear();
            _files = null;
        }
    }
}
