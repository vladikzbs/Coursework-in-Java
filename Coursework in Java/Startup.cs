using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Coursework_in_Java.Startup))]
namespace Coursework_in_Java
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
