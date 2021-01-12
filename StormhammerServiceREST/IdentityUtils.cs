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
        /*public static void CreateIdentityIfDoesntExist(StormhammerContext dbContext, Guid objectId)
        {
            var identity = dbContext.Identity.FirstOrDefault(e => e.ObjectId.ToString().ToUpper().Equals(objectId.ToString().ToUpper()));
            if (identity == null)
            {
                identity = dbContext.Identity.Add(new Identity() { ObjectId = objectId }).Entity;
                dbContext.SaveChanges();
            }
        }*/
    }
}
