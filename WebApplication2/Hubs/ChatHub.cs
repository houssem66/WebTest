using Domaine.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourMe.Web.Models;

namespace TourMe.Web.Hubs
{
    public class ChatHub : Hub
    {
        private readonly UserManager<Utilisateur> userManager;
        private readonly IWebHostEnvironment hostingEnvironment;

        public ChatHub(UserManager<Utilisateur> userManager, IWebHostEnvironment hostingEnvironment)
        {
            this.userManager = userManager;
            this.hostingEnvironment = hostingEnvironment;
        }
        public override Task OnConnectedAsync()
        {
            base.OnConnectedAsync();
            var user = Context.User.Identity.Name;
            //Groups.AddAsync(Context.ConnectionId, user);

            return Task.CompletedTask;
        }
        public async Task SendMessage(string user,string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
