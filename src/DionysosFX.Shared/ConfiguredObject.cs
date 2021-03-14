using System;

namespace DionysosFX.Shared
{
    public class ConfiguredObject
    {
        readonly object _syncRoot = new();
        bool _configurationLocked;

        protected bool ConfigurationLocked
        {
            get
            {
                lock (_syncRoot)
                {
                    return _configurationLocked;
                }
            }
        }

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

        protected virtual void OnBeforeConfiguration()
        {
        }

        public void EnsureConfigurationNotLocked()
        {
            if (ConfigurationLocked)
                throw new InvalidOperationException($"{GetType().Name} Instance is locked.");
        }
    }
}
