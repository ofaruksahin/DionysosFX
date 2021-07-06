using System.Collections.Generic;
using System.Linq;

namespace DionysosFX.Host
{
    /// <summary>
    /// 
    /// </summary>
    public class HostBuilder : IHostBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        List<string> _prefixes = null;

        /// <summary>
        /// 
        /// </summary>
        public IReadOnlyList<string> Prefixes => _prefixes;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        public void AddPrefix(string prefix)
        {
            if (!_prefixes.Any(f => f == prefix))
                _prefixes.Add(prefix);
        }
    }
}
