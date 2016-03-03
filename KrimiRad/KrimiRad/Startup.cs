using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KrimiRad.Startup))]
namespace KrimiRad
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
