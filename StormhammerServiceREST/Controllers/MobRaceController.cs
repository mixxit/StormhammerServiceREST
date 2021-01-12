using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

    public class MobRaceController : ControllerBase
    {
        private readonly ILogger<MobRaceController> _logger;
        private StormhammerContext _dbContext;
        public MobRaceController(ILogger<MobRaceController> logger, StormhammerContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<List<MobRace>> Get()
        {
            // stub
            return new OkObjectResult(_dbContext.MobRace);
        }
    }
}
