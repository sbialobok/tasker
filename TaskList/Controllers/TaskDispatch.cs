using System;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;

namespace TaskList.Controllers
{
    /// <summary>
    /// To be used to send out updates about Task updates.
    /// </summary>
    public class TaskDispatch : PersistentConnection
    {
        protected override Task OnConnected(IRequest request, string connectionId)
        {
            return Connection.Broadcast(connectionId + " has connected!!");
        }
    }
}
