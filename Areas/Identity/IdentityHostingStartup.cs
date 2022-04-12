using Microsoft.AspNetCore.Hosting;


[assembly: HostingStartup(typeof(Itransition.Areas.Identity.IdentityHostingStartup))]
namespace Itransition.Areas.Identity
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