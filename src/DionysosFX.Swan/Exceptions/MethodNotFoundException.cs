using System;

namespace DionysosFX.Swan.Exceptions
{
    /// <summary>
    /// Type get method not found after than throw MethodNotFound exception
    /// </summary>
    public class MethodNotFoundException : Exception
    {
        public MethodNotFoundException(string methodName) : base($"{methodName} Method Not Found")
        {

        }
    }
}
