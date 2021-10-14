namespace DionysosFX.Module.HealthCheck.SqlServer
{
    public class SqlServerHealthCheckOptions
    {
        public string ConnectionString
        {
            get;
            set;
        }

        public string Sql
        {
            get;
            set;
        }
    }
}
