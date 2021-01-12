﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private StormhammerContext _dbContext;
        public LoginController(ILogger<LoginController> logger, StormhammerContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpPost]
        public ActionResult<LoginResponse> Login(LoginRequest request)
        {
            if (request == null || String.IsNullOrEmpty(request.UserName) || String.IsNullOrEmpty(request.Password) || String.IsNullOrEmpty(request.UniqueId))
                return new BadRequestResult();

            // stub
            return new OkObjectResult(TryLogin(request));
        }

        private LoginResponse TryLogin(LoginRequest request)
        {
            IdentityUtils.CreateIdentityIfDoesntExist(_dbContext, request.UniqueId);

            var identity = _dbContext.Identity.FirstOrDefault(e => e.UniqueId.Equals(request.UniqueId));
            if (String.IsNullOrEmpty(identity.Username))
            {
                if (_dbContext.Identity.Any(e => e.Username.Equals(request.UserName)))
                    return new LoginResponse() { LoggedIn = false, SessionId = null };

                identity.Username = request.UserName;
                identity.Password = request.Password;
                _dbContext.SaveChanges();
            }

            if (!_dbContext.Identity.Any(e => e.Username.Equals(request.UserName) && request.Password.Equals(request.Password) && e.UniqueId.Equals(request.UniqueId)))
                return new LoginResponse() { LoggedIn = false, SessionId = null };

            var sessionId = Guid.NewGuid().ToString();
            identity.SessionId = sessionId;
            _dbContext.SaveChanges();

            return new LoginResponse() { LoggedIn = true, SessionId = sessionId };
        }
    }
}