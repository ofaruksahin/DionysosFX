using System;

namespace DionysosFX.Module.HealthCheck.SqlServer
{
    public class SqlServerHealthCheck : IHealthCheck
    {
        private SqlServerHealthCheckOptions options;

        public SqlServerHealthCheck(SqlServerHealthCheckOptions _options)
        {
            options = _options;
        }

        public HealthCheckResponse IsHealthily()
        {
            DateTime startDate = DateTime.Now;
            return new HealthilyResponse(DateTime.Now, DateTime.Now);
        }
    }
}
