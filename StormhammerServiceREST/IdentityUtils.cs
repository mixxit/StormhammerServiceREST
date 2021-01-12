using StormhammerLibrary.Models;
using StormhammerLibrary.Models.Request;
using StormhammerLibrary.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StormhammerServiceREST
{
    public class IdentityUtils
    {
        public static RegisterResponse CreateIdentityIfDoesntExist(StormhammerContext dbContext, string uniqueId)
        {
            var identity = dbContext.Identity.FirstOrDefault(e => e.UniqueId.Equals(uniqueId));
            if (identity == null)
            {
                identity = dbContext.Identity.Add(new Identity() { UniqueId = uniqueId }).Entity;
                dbContext.SaveChanges();
            }

            return new RegisterResponse()
            {
                Registered = true
            };
        }
    }
}
