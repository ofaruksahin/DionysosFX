using System;

namespace DionysosFX.Module.StaticFile
{
    /// <summary>
    /// Static File Object
    /// </summary>
    internal class StaticFileItem
    {
        /// <summary>
        /// File bytes
        /// </summary>
        public byte[] Data
        {
            get;
            set;
        }

        /// <summary>
        /// File cache expire date
        /// </summary>
        public DateTime ExpireDate
        {
            get;
            set;
        }

        public StaticFileItem(byte[] data,DateTime expireDate)
        {
            Data = data;
            ExpireDate = expireDate;
        }
    }
}
