using System.Web.ApplicationServices;
using EBBS.Models;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EBBS.Startup))]

namespace EBBS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            //var config = new AutoMapper.MapperConfiguration(c => { c.AddProfile(new AutoMapperProfile()); });


        }
    }
}
