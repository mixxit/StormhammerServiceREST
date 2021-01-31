﻿using Microsoft.AspNetCore.SignalR;
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
        public async Task GetMobRaces()
        {
            var identityView = IdentityView.FromObjectId(_dbContext, (SHIdentity.FromPrincipal(Context.User)).ObjectId);
            if (identityView.Identity == null)
                return;
            if (!Context.User.Identity.IsAuthenticated)
                return;

            if (_dbContext.MobRace != null)
                await Clients.Client(Context.ConnectionId).SendAsync("MobRacesResponse", "", _dbContext.MobRace.ToList());
            else
                await Clients.Client(Context.ConnectionId).SendAsync("MobRacesResponse", "", new List<MobRace>());
        }
    }
}
