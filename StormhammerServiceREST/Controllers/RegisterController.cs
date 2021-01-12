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
    public class RegisterController : ControllerBase
    {
        private readonly ILogger<MobClassController> _logger;

        private StormhammerContext _dbContext;
        public RegisterController(ILogger<MobClassController> logger, StormhammerContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpPost]
        public ActionResult<RegisterResponse> Register(RegisterRequest request)
        {
            if (request == null || String.IsNullOrEmpty(request.UniqueId))
                return new BadRequestResult();

            return new OkObjectResult(IdentityUtils.CreateIdentityIfDoesntExist(_dbContext, request.UniqueId));
        }
    }
}
