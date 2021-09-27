using System;

namespace DionysosFX.Swan.Exceptions
{
    public class OptionsNotFoundException : Exception
    {
        public OptionsNotFoundException(string optionsName) : base($"{optionsName} Not Found")
        {

        }
    }
}
