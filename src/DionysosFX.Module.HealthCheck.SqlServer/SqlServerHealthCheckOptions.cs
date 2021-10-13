namespace DionysosFX.Module.HealthCheck.SqlServer
{
    public class SqlServerHealthCheckOptions
    {
        public string ServerAddress
        {
            get;
            set;
        }

        public string UserName
        {
            get;
            set;
        }

        public string Password
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
