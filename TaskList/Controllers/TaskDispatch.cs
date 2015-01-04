using System;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;

namespace TaskList.Controllers
{
    public class TaskDispatch : PersistentConnection
    {
        protected override Task OnConnected(IRequest request, string connectionId)
        {
            return Connection.Broadcast(connectionId + " has connected!!");
        }

        private 
    }
}
