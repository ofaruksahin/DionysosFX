using System.Collections.Generic;

namespace DionysosFX.Host
{
    /// <summary>
    /// 
    /// </summary>
    public interface IHostBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        IReadOnlyList<string> Prefixes
        { 
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        void AddPrefix(string prefix);
    }
}
