using Microsoft.Extensions.Configuration;
using SeiyuuSync.Utils;

namespace SeiyuuSync
{
    internal static class Program
    {
        public static IConfiguration Configuration;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Set up configuration builder
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);
               
            Configuration = builder.Build();

            // Load in env variables
            Constants.ACCESS_TOKEN = Configuration.GetSection("access_token").Get<string>();

            ApplicationConfiguration.Initialize();
            Application.Run(new HomeForm());
        }
    }
}