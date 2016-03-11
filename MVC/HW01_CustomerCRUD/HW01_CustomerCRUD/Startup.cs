using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HW01_CustomerCRUD.Startup))]
namespace HW01_CustomerCRUD
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
