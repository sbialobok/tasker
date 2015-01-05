using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
using TaskList.Controllers;

[assembly: OwinStartup(typeof(TaskList.Startup))]
namespace TaskList
{
    public class Startup
    {
        /// <summary>
        /// Gets called on Owin startup to configure the server side of the signalr connection
        /// </summary>
        /// <param name="app"></param>
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR("/updates", typeof(TaskDispatch), new ConnectionConfiguration());
        }
    }
}