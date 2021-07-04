namespace DionysosFX.WebApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateStartup();
        }       

        static void CreateStartup()
        {
            Host.Host host = new Host.Host();
            var startup = host.CreateStartup<Startup>();
            startup.Configure();
            startup.Build();
        }
    }
}
