using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(QuickCode.Startup))]
namespace QuickCode
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
