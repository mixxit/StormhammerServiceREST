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
    public class MobRaceController : ControllerBase
    {
        private readonly ILogger<MobRaceController> _logger;
        private readonly WorldRepository _worldRepository;

        public MobRaceController(ILogger<MobRaceController> logger, WorldRepository worldRepository)
        {
            _logger = logger;
            _worldRepository = worldRepository;
        }

        [HttpGet]
        public ActionResult<List<MobRace>> Get()
        {
            // stub
            return new OkObjectResult(_worldRepository.MobRace);
        }
    }
}
