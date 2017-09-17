using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ESportsEventsApp.Startup))]
namespace ESportsEventsApp
{
    using System.Globalization;
    using System.Threading;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
            this.ConfigureRoles();
        }
    }
}
