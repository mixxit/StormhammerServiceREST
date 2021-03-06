﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using StormhammerLibrary.Models;
using StormhammerServiceREST.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StormhammerServiceREST.Hubs
{
    public partial class WorldHub : Hub
    {
        [Authorize]
        public async Task GetMobsByOwnerId()
        {
            var identityView = IdentityView.FromObjectId(_dbContext, (SHIdentity.FromPrincipal(Context.User)).ObjectId);
            if (identityView.Identity == null)
                return;
            if (!Context.User.Identity.IsAuthenticated)
                return;

            var mobs = _dbContext.Mob.Where(e => e.AccountId == identityView.Identity.Id);
            if (mobs != null)
                await Clients.Client(Context.ConnectionId).SendAsync("MobsByOwnerIdResponse", "", JsonConvert.SerializeObject(mobs.ToList()));
            else
                await Clients.Client(Context.ConnectionId).SendAsync("MobsByOwnerIdResponse", "", JsonConvert.SerializeObject(new List<Mob>()));
        }
    }
}
