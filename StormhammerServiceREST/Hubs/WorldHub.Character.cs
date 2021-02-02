using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using StormhammerLibrary.Models;
using StormhammerLibrary.Models.Request;
using StormhammerLibrary.Models.Response;
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
        public async Task CreateCharacter(CreateCharacterRequest request)
        {
            var identityView = IdentityView.FromObjectId(_dbContext, (SHIdentity.FromPrincipal(Context.User)).ObjectId);
            if (identityView.Identity == null)
                return;
            if (!Context.User.Identity.IsAuthenticated)
                return;

            if (request == null || String.IsNullOrEmpty(request.Name))
                return;

            if (_dbContext.Mob.Count(e => e.AccountId == identityView.Identity.Id) > 2)
                return;

            var mob = new Mob()
            {
                MobClassId = request.MobClassId,
                MobRaceId = request.MobRaceId,
                AccountId = identityView.Identity.Id,
                Name = request.Name,
                ZoneId = 1
            };

            if (_dbContext.Mob.Any(e => e.AccountId == identityView.Identity.Id && e.Name.Equals(mob.Name)))
                return;

            mob = _dbContext.Mob.Add(mob).Entity;
            _dbContext.SaveChanges();
            var response = new CreateCharacterResponse();
            response.Mob = mob;

            await Clients.Client(Context.ConnectionId).SendAsync("CreateCharacterResponse", "", JsonConvert.SerializeObject(response));
        }

        [Authorize]
        public async Task DeleteCharacter(long id)
        {
            var identityView = IdentityView.FromObjectId(_dbContext, (SHIdentity.FromPrincipal(Context.User)).ObjectId);
            if (identityView.Identity == null)
                return;
            if (!Context.User.Identity.IsAuthenticated)
                return;

            if (id < 1)
                return;

            var mob = _dbContext.Mob.FirstOrDefault(e => e.Id == id && e.AccountId == identityView.Identity.Id);

            if (mob == null)
                return;

            _dbContext.Mob.Remove(mob);
            _dbContext.SaveChanges();

            await Clients.Client(Context.ConnectionId).SendAsync("DeleteCharacterResponse", "", JsonConvert.SerializeObject(id));
        }
    }
}
