using System;

namespace DionysosFX.Host
{
    /// <summary>
    /// Triggered When the state of the web server changes 
    /// </summary>
    public class WebServerStateChangeEventArgs : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldState">Web server old state</param>
        /// <param name="newState">Web server new state</param>
        public WebServerStateChangeEventArgs(WebServerState oldState,WebServerState newState)
        {
            OldState = oldState;
            NewState = newState;
        }

        /// <summary>
        /// Web server old state
        /// </summary>
        public WebServerState OldState
        {
            get;
            set;
        }

        /// <summary>
        /// Web server new state
        /// </summary>
        public WebServerState NewState
        {
            get;
            set;
        }
    }
}
