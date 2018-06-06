using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IdentityLoginPage.Startup))]
namespace IdentityLoginPage
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
