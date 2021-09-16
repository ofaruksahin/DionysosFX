using System;
using System.Collections.Generic;

namespace DionysosFX.Module.StaticFile
{
    /// <summary>
    /// Static file options
    /// </summary>
    public class StaticFileOptions
    {
        /// <summary>
        /// Allowed mime types
        /// </summary>
        private List<string> _allowedMimeTypes = null;

        public List<string> AllowedMimeTypes
        {
            get => _allowedMimeTypes ?? (_allowedMimeTypes = new List<string>());
        }

        /// <summary>
        /// Cache Expire Time
        /// </summary>
        public int ExpireTime
        {
            get;
            set;
        }

        /// <summary>
        /// Cache is active
        /// </summary>
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
