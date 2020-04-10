using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Delu.Delicia.Site.Startup))]
namespace Delu.Delicia.Site
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
