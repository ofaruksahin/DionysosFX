using DionysosFX.Swan.Modules;
using System.Collections.Generic;
using System.Threading;

namespace DionysosFX.Module.HealthCheck
{
    internal class HealthCheckModule : WebModuleBase
    {
        private List<IHealthCheck> _healthChecks;
        public List<IHealthCheck> HealthChecks
        {
            get => _healthChecks ?? (_healthChecks = new List<IHealthCheck>());
            set => _healthChecks = value;
        }

        public override void Start(CancellationToken cancellationToken)
        {

        }

        public override void Dispose()
        {

        }
    }
}
