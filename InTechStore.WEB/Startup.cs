using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(InTechStore.WEB.Startup))]
namespace InTechStore.WEB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
