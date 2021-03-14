using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DionysosFX
{
    public interface IHttpListener : IDisposable
    {
        List<string> Prefixes { get; }
        bool IsListening { get; }
        string Name { get; }
        void Start();
        void Stop();
        void AddPrefix(string urlPrefix);
    }
}
