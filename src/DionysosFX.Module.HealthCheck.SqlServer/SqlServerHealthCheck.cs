using System;
using System.Data;
using System.Data.SqlClient;

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
            DateTime endDate = new DateTime();
            try
            {
                using (SqlConnection con = new SqlConnection(options.ConnectionString))
                {
                    if (con.State != ConnectionState.Open)
                        con.Open();
                    using (SqlCommand cmd = new SqlCommand(options.Sql, con))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    if (con.State == ConnectionState.Open)
                        con.Close();
                }
                endDate = DateTime.Now;
                return new HealthilyResponse(startDate, endDate);
            }
            catch (Exception e)
            {
                return new UnHealthilyResponse(startDate, endDate, e.Message);
            }
        }
    }
}
