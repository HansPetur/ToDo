using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProjectToDoList.Startup))]
namespace ProjectToDoList
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
