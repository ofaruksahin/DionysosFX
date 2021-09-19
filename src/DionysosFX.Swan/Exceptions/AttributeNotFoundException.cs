using System;

namespace DionysosFX.Swan.Exceptions
{
    public class AttributeNotFoundException : Exception
    {
        public AttributeNotFoundException(string attributeName) : base($"{attributeName} Attribute not found")
        {

        }
    }
}
