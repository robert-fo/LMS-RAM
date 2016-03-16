using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LMS_RAM.Startup))]
namespace LMS_RAM
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
