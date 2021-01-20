using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StormhammerLibrary.Models;
using StormhammerLibrary.Models.Request;
using StormhammerLibrary.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StormhammerServiceREST.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]

    public class MobController : ControllerBase
    {
        private readonly ILogger<MobController> _logger;
        private StormhammerContext _dbContext;
        public MobController(ILogger<MobController> logger, StormhammerContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        /*[HttpGet]
        public ActionResult<Mob> Get(long Id)
        {
            // stub
            return new OkObjectResult(_dbContext.Mob.FirstOrDefault(e => e.Id == Id));
        }*/

        [HttpGet("ByOwned")]
        public ActionResult<List<Mob>> GetByOwned()
        {
            var identityView = IdentityView.FromObjectId(_dbContext, (SHIdentity.FromPrincipal(User)).ObjectId);
            if (identityView.Identity == null)
                return new UnauthorizedResult();

            // stub
            return new OkObjectResult(_dbContext.Mob.Where(e => e.AccountId == identityView.Identity.Id));
        }


    }
}
