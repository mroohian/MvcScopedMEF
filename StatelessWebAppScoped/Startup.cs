using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StatelessWebAppScoped.Startup))]
namespace StatelessWebAppScoped
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
