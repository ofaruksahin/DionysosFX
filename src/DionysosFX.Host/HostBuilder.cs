using System.Collections.Generic;
using System.Linq;

namespace DionysosFX.Host
{
    public class HostBuilder : IHostBuilder
    {
        List<string> _prefixes = null;
        List<string> Prefixes => _prefixes ?? new();

        public void AddPrefix(string prefix)
        {
            if (!Prefixes.Any(f => f == prefix))
                Prefixes.Add(prefix);
        }
    }
}
