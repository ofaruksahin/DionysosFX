using System;

namespace DionysosFX.Swan.Routing
{
    public class UrlFormatException : Exception
    {
        public UrlFormatException(string route) : base($"{route} is not valid url")
        {

        }       
    }
}
