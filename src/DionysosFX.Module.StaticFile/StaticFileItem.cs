using System;

namespace DionysosFX.Module.StaticFile
{
    internal class StaticFileItem
    {
        public byte[] Data
        {
            get;
            set;
        }

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
