using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StormhammerLibrary.Models;
using StormhammerLibrary.Models.Request;
using StormhammerLibrary.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace StormhammerServiceREST.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class CharacterController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private StormhammerContext _dbContext;
        public CharacterController(ILogger<LoginController> logger, StormhammerContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }
        /*
        [HttpPost]
        public ActionResult<Mob> CreateCharacter(CreateCharacterRequest request)
        {
            var identityView = IdentityView.FromObjectId(this._dbContext, (ApplicationUser.FromPrincipal(User)).ObjectId);
            if (identityView.Identity == null)
                return new UnauthorizedResult();

            if (request == null || String.IsNullOrEmpty(request.Name))
                return new BadRequestResult();

            var mob = new Mob()
            {
                MobClassId = request.MobClassId,
                MobRaceId = request.MobRaceId,
                OwnerId = identityView.Identity.Id,
                Name = request.Name
            };

            if (_dbContext.Mob.Any(e => e.OwnerId == identityView.Identity.Id && e.Name.Equals(mob.Name)))
                return new ConflictResult();

            mob = _dbContext.Mob.Add(mob).Entity;
            _dbContext.SaveChanges();
            return new OkObjectResult(mob);
        }*/
    }
}
