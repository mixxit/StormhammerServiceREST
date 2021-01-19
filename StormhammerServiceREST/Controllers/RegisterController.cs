using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StormhammerLibrary.Models;
using StormhammerLibrary.Models.Request;
using StormhammerLibrary.Models.Response;
using StormhammerServiceREST.ViewModel;
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
    [Route("[controller]")]
    public class RegisterController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private StormhammerContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public RegisterController(ILogger<AccountController> logger, StormhammerContext dbContext, UserManager<IdentityUser> userManager,
                              SignInManager<IdentityUser> signInManager)
        {
            _logger = logger;
            _dbContext = dbContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return new OkObjectResult(result);
                }

                var errors = "";
                if (result?.Errors != null && result.Errors.Count() > 0)
                    errors = String.Join(",", result.Errors.Select(e => e.Description));
                return new UnauthorizedObjectResult($"Invalid Login Attempt [{errors}]");

            }

            return new OkObjectResult(model);
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
