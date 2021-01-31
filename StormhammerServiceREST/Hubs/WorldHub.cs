using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using StormhammerServiceREST.Controllers;
using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StormhammerServiceREST.ViewModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using StormhammerLibrary.Models.Response;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using StormhammerLibrary.Models;

namespace StormhammerServiceREST.Hubs
{
    public partial class WorldHub : Hub
    {
        private readonly ILogger<WorldHub> _logger;
        private StormhammerContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;
        public WorldHub(ILogger<WorldHub> logger, StormhammerContext dbContext, UserManager<IdentityUser> userManager,
                              SignInManager<IdentityUser> signInManager, IConfiguration configuration)
        {
            _logger = logger;
            _dbContext = dbContext;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task LoginRequest(LoginViewModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim("UserId", user.Id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:Key"])), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);

                await Clients.Client(Context.ConnectionId).SendAsync("LoginResponse", "", token);
            }
            else
            {
                await Clients.Client(Context.ConnectionId).SendAsync("LoginResponse", "", "");
            }
        }

    }
}
