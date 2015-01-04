using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
using TaskList.Controllers;

[assembly: OwinStartup(typeof(TaskList.Startup))]
namespace TaskList
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR("/updates", typeof(TaskDispatch), new ConnectionConfiguration());
        }
    }
}