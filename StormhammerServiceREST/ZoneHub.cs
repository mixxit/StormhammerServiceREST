using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StormhammerServiceREST.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;

namespace StormhammerServiceREST
{
    [Authorize]
    public class ZoneHub : Hub
    {
        private readonly ILogger<ZoneHub> _logger;
        private StormhammerContext _dbContext;
        public ZoneHub(ILogger<ZoneHub> logger, StormhammerContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public void HelloMethod(String line)
        {
            var identityView = IdentityView.FromObjectId(_dbContext, (SHIdentity.FromPrincipal(Context.User)).ObjectId);
            if (identityView.Identity == null)
                return;

            if (!Context.User.Identity.IsAuthenticated)
                return;

            Clients.All.SendAsync("ReceiveMessage", identityView.Identity.ObjectId, line);
        }
    }
}
