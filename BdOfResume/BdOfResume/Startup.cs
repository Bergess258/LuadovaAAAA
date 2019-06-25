using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BdOfResume.Startup))]
namespace BdOfResume
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
