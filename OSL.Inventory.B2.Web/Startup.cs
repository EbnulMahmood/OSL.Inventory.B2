using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OSL.Inventory.B2.Web.Startup))]
namespace OSL.Inventory.B2.Web
{
    public partial class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Register services here through dependency injection
        }

        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
