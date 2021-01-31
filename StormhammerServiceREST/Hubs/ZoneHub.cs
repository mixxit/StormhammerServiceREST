using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using StormhammerServiceREST.Controllers;
using System;
using System.Web.Http;

namespace StormhammerServiceREST.Hubs
{
    [Authorize]
    public partial class ZoneHub : Hub
    {
        private readonly ILogger<ZoneHub> _logger;
        private StormhammerContext _dbContext;
        public ZoneHub(ILogger<ZoneHub> logger, StormhammerContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }
    }
}
