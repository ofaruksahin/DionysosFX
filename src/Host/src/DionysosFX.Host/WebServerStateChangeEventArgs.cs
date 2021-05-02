using System;

namespace DionysosFX.Host
{
    public class WebServerStateChangeEventArgs : EventArgs
    {
        public WebServerStateChangeEventArgs(WebServerState oldState, WebServerState newState)
        {
            OldState = oldState;
            NewState = newState;
        }

        public WebServerState OldState { get; set; }
        public WebServerState NewState { get; set; }
    }
}
