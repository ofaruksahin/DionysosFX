using System;

namespace DionysosFX.Swan
{
    public class PlatformNotSupportedException : Exception
    {
        public PlatformNotSupportedException(string name) : base($"{name} is only available windows")
        {

        }
    }
}
