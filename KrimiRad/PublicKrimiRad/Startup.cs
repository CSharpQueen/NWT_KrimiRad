using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PublicKrimiRad.Startup))]
namespace PublicKrimiRad
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
