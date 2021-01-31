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
        public async Task GetMobsByOwnerId()
        {
            var identityView = IdentityView.FromObjectId(_dbContext, (SHIdentity.FromPrincipal(Context.User)).ObjectId);
            if (identityView.Identity == null)
                return;
            if (!Context.User.Identity.IsAuthenticated)
                return;

            var mobs = _dbContext.Mob.Where(e => e.AccountId == identityView.Identity.Id);
            if (mobs != null)
                await Clients.Client(Context.ConnectionId).SendAsync("MobsByOwnerIdResponse", "", mobs.ToList());
            else
                await Clients.Client(Context.ConnectionId).SendAsync("MobsByOwnerIdResponse", "", new List<Mob>());
        }
    }
}
