namespace DionysosFX.Host.Net.Internal
{
    internal class SystemHttpListener
    {
        private System.Net.HttpListener _listener;

        public SystemHttpListener(System.Net.HttpListener listener)
        {
            _listener = listener;
        }
    }
}
