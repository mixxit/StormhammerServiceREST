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
    public class MobClassController : ControllerBase
    {
        private readonly ILogger<MobClassController> _logger;
        private StormhammerContext _dbContext;
        public MobClassController(ILogger<MobClassController> logger, StormhammerContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<List<MobClass>> Get()
        {
            // stub
            return new OkObjectResult(_dbContext.MobClass);
        }
    }
}
