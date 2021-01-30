using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StormhammerServiceREST
{
    public class UserIdClaimProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            var claims = connection.User.Claims.ToList();
            return claims.FirstOrDefault(x => x.Type == "UserId")?.Value;
        }
    }
}
