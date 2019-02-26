using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OBS.MVC.Startup))]
namespace OBS.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
