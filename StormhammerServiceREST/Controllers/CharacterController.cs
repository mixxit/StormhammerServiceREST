﻿using Microsoft.AspNetCore.Authorization;
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
        private readonly ILogger<AccountController> _logger;
        private StormhammerContext _dbContext;
        public CharacterController(ILogger<AccountController> logger, StormhammerContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpPost]
        public ActionResult<Mob> CreateCharacter(CreateCharacterRequest request)
        {
            var identityView = IdentityView.FromObjectId(_dbContext, (SHIdentity.FromPrincipal(User)).ObjectId);
            if (identityView.Identity == null)
                return new UnauthorizedResult();

            if (request == null || String.IsNullOrEmpty(request.Name))
                return new BadRequestResult();

            if (_dbContext.Mob.Count(e => e.AccountId == identityView.Identity.Id) > 2)
                return new BadRequestObjectResult("Limit of 3 characters");

            var mob = new Mob()
            {
                MobClassId = request.MobClassId,
                MobRaceId = request.MobRaceId,
                AccountId = identityView.Identity.Id,
                Name = request.Name
            };

            if (_dbContext.Mob.Any(e => e.AccountId == identityView.Identity.Id && e.Name.Equals(mob.Name)))
                return new ConflictResult();

            mob = _dbContext.Mob.Add(mob).Entity;
            _dbContext.SaveChanges();
            return new OkObjectResult(mob);
        }
    }
}
