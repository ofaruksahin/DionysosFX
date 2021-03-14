using System;

namespace DionysosFX.Shared
{
    public class PlatformNotSupportedException : Exception
    {
        public PlatformNotSupportedException(string name) : base($"{name} is only available windows")
        {

        }
    }
}
