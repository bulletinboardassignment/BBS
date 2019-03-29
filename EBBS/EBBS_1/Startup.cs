using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EBBS_1.Startup))]
namespace EBBS_1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //    ConfigureAuth(app);
            //
        }
    }
}
