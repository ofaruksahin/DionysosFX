using DionysosFX.Swan.Net;
using System.IO;

namespace DionysosFX.Host.Net.Internal
{
    /// <summary>
    /// 
    /// </summary>
    internal class SystemHttpResponse : IHttpResponse
    {
        private System.Net.HttpListenerResponse _response;

        public SystemHttpResponse(System.Net.HttpListenerResponse response)
        {
            _response = response;
        }

        public Stream Body => _response.OutputStream;

        public void Close()
        {
            _response.Close();
        }
    }
}
