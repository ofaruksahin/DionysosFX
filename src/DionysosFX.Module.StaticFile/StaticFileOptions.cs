using System;
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

        public int ExpireTime
        {
            get;
            set;
        }

        public bool CacheActive
        {
            get;
            set;
        }

        public StaticFileOptions()
        {
            ExpireTime = (int)TimeSpan.FromMinutes(5).TotalSeconds;
            CacheActive = true;
        }
    }
}
