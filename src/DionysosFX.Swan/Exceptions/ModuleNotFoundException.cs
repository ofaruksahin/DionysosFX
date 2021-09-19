using System;

namespace DionysosFX.Swan.Exceptions
{
    public class ModuleNotFoundException : Exception
    {
        public ModuleNotFoundException(string moduleName) : base($"{moduleName} module is not found")
        {

        }
    }
}
