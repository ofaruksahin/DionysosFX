using System.Collections.Generic;

namespace DionysosFX.Module.StaticFile
{
    public class StaticFileOptions
    {
        private List<string> _allowedMimeTypes = null;

        public List<string> AllowedMimeTypes
        {
            get => _allowedMimeTypes ?? (_allowedMimeTypes = new List<string>());
        }
    }
}
