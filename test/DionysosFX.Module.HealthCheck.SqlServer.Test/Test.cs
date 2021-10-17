using System;
using Xunit;

namespace DionysosFX.Module.HealthCheck.SqlServer.Test
{
    public class Test
    {
        private string ConnectionString1 = "Server=192.168.1.103;Database=test;User Id=sa;Password=123456789;";
        private string ConnectionString2 = "Server=192.168.1.103;Database=test;User Id=sa;Password=*;";

        [Fact]
        public void HealthilyTest()
        {
            SqlServerHealthCheckOptions options = new SqlServerHealthCheckOptions();
            options.ConnectionString = ConnectionString1;
            options.Sql = "SELECT 1";
            SqlServerHealthCheck healthCheck = new SqlServerHealthCheck(options);
            var response = healthCheck.IsHealthily();
            Assert.True(response.GetType() == typeof(HealthilyResponse));
        }

        [Fact]
        public void UnHealthilyTest()
        {
            SqlServerHealthCheckOptions options = new SqlServerHealthCheckOptions();
            options.ConnectionString = ConnectionString2;
            options.Sql = "SELECT 1";
            SqlServerHealthCheck healthCheck = new SqlServerHealthCheck(options);
            var response = healthCheck.IsHealthily();
            Assert.True(response.GetType() == typeof(UnHealthilyResponse));
        }
    }
}
