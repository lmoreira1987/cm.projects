using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Delu.Delicia.MVC.Startup))]
namespace Delu.Delicia.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
