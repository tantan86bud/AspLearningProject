using AspLearningProject.Models.DataLayer.Migration;
using Serilog;

namespace AspLearningProject
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = CreateHostBuilder(args);
            builder.UseSerilog((hcxt, prov, config) => config.ReadFrom.Configuration(hcxt.Configuration));
            builder.Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
