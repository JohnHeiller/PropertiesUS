using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace API.PropertiesUS
{
    /// <summary>
    /// Main base class
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Method for the initial construction of the project
        /// </summary>
        /// <param name="args">framework arguments</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Method to invoke the startup class
        /// </summary>
        /// <param name="args">framework arguments</param>
        /// <returns>Object of type IHostBuilder</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
