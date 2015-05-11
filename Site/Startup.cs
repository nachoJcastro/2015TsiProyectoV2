using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Site.Startup))]
namespace Site
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
