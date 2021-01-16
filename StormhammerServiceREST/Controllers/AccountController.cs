using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StormhammerLibrary.Models;
using StormhammerLibrary.Models.Request;
using StormhammerLibrary.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace StormhammerServiceREST.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private StormhammerContext _dbContext;
        public AccountController(ILogger<AccountController> logger, StormhammerContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<Account> GetIdentity()
        {
            var identityView = IdentityView.FromObjectId(_dbContext, (SHIdentity.FromPrincipal(User)).ObjectId);
            return new OkObjectResult(identityView);
        }

        /*[HttpGet]
        public ActionResult<Identity> GetIdentity()
        {
            var identityView = IdentityView.FromObjectId(this._dbContext, (Identity.FromPrincipal(User)).ObjectId);
            if (identityView.Identity == null)
                return new UnauthorizedResult();

            return new OkObjectResult(identityView.Identity);
        }*/

        /*
        [HttpPost]
        public ActionResult<bool> Login()
        {
            var identityView = IdentityView.FromObjectId(this._dbContext, (Identity.FromPrincipal(User)).ObjectId);
            IdentityUtils.CreateIdentityIfDoesntExist(_dbContext, identityView.ObjectId);
            var identity = _dbContext.Identity.FirstOrDefault(e => e.ObjectId.Equals(identityView.ObjectId));
            var sessionId = Guid.NewGuid().ToString();
            identity.SessionId = sessionId;
            _dbContext.SaveChanges();
            // stub
            return new OkResult();
        }
        */
    }
}
