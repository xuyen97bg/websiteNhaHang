using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebsiteQL_Nha_Hang.Startup))]
namespace WebsiteQL_Nha_Hang
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
