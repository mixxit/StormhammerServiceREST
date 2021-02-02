using Microsoft.AspNetCore.Authorization;
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
        public async Task GetMobClasses()
        {
            var identityView = IdentityView.FromObjectId(_dbContext, (SHIdentity.FromPrincipal(Context.User)).ObjectId);
            if (identityView.Identity == null)
                return;
            if (!Context.User.Identity.IsAuthenticated)
                return;

            if (_dbContext.MobClass != null)
                await Clients.Client(Context.ConnectionId).SendAsync("MobClassesResponse", "", JsonConvert.SerializeObject(_dbContext.MobClass.ToList()));
            else
                await Clients.Client(Context.ConnectionId).SendAsync("MobClassesResponse", "", JsonConvert.SerializeObject(new List<MobClass>()));
        }
    }
}
