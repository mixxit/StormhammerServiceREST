using StormhammerLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StormhammerServiceREST
{
    /*public class IdentityView
    {
        public Guid ObjectId { get; set; }
        public Identity Identity { get; set; }

        public static IdentityView FromObjectId(StormhammerContext dbContext, Guid objectId)
        {
            IdentityView identityView = new IdentityView();
            identityView.ObjectId = objectId;
            identityView.Identity = dbContext.Identity
                .FirstOrDefault(e => e.ObjectId.ToString().ToUpper().Equals(identityView.ObjectId.ToString().ToUpper()));
            return identityView;
        }

    }*/
}
