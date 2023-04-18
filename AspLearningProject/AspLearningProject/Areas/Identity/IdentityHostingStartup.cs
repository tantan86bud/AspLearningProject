

[assembly: HostingStartup(typeof(AspLearningProject.Areas.Identity.IdentityHostingStartup))]
namespace AspLearningProject.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}
