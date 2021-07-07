using System.IO;

namespace DionysosFX.Swan.Net
{
    /// <summary>
    /// 
    /// </summary>
    public interface IHttpResponse
    {
        Stream Body
        {
            get;
        }
        void Close();
    }
}
