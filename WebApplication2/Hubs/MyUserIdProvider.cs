using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace TourMe.Web.Hubs
{
    public class MyUserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            return connection.User.Identity.Name;
        }
    }
}