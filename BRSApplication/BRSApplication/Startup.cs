using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BRSApplication.Startup))]
namespace BRSApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
