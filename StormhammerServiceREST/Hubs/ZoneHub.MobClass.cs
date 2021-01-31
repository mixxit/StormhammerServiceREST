using Microsoft.AspNetCore.SignalR;
using StormhammerLibrary.Models;
using StormhammerServiceREST.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StormhammerServiceREST.Hubs
{
    public partial class ZoneHub : Hub
    {
        public async Task GetMobClasses()
        {
            var identityView = IdentityView.FromObjectId(_dbContext, (SHIdentity.FromPrincipal(Context.User)).ObjectId);
            if (identityView.Identity == null)
                return;
            if (!Context.User.Identity.IsAuthenticated)
                return;

            await Clients.Client(Context.ConnectionId).SendAsync("MobClassesResponse", "", _dbContext.MobClass.ToList());
        }
    }
}
