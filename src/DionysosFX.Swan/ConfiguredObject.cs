using System;

namespace DionysosFX.Swan
{
    /// <summary>
    /// 
    /// </summary>
    public class ConfiguredObject
    {
        /// <summary>
        /// 
        /// </summary>
        readonly object _syncRoot = new();
        /// <summary>
        /// 
        /// </summary>
        bool _configurationLocked;

        /// <summary>
        /// 
        /// </summary>
        protected bool ConfigurationLocker
        {
            get
            {
                lock (_syncRoot)
                {
                    return _configurationLocked;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool ConfigurationLocked
        {
            get;
            private set;
        }

        /// <summary>
        /// 
        /// </summary>
        protected void LockConfiguration()
        {
            lock (_syncRoot)
            {
                if (_configurationLocked)
                    return;
                OnBeforeConfiguration();
                _configurationLocked = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void OnBeforeConfiguration()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public void EnsureConfigurationNotLocked()
        {
            if (ConfigurationLocked)
                throw new InvalidOperationException($"{GetType().Name} Is Locked");
        }
    }
}
