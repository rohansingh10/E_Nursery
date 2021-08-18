using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(E_Nursery.Startup))]
namespace E_Nursery
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
