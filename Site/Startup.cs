
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Site.Startup))]
namespace Site
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();

            ConfigureAuth(app);
        }
    }
}
