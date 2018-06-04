using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CapstoneReworked.Startup))]
namespace CapstoneReworked
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
