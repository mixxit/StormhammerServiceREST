using System;
using System.Security.Claims;

namespace StormhammerServiceREST.Controllers
{
    public class SHIdentity
    {
        public string EmailAddress { get; internal set; }
        public string Issuer { get; internal set; }
        public DateTime AuthTimeUTC { get; internal set; }
        public DateTime ExpiryUTC { get; internal set; }
        public string ClaimedCompany { get; internal set; }
        public string Misc { get; set; }
        public Guid ObjectId { get; private set; }
        public string IdentityProvider { get; private set; }
        public static SHIdentity FromPrincipal(ClaimsPrincipal principal)
        {
            SHIdentity identity = new SHIdentity();

            foreach (Claim claim in principal.Claims)
            {
                switch (claim.Type)
                {
                    case "auth_time":
                        DateTimeOffset dateTimeOffsetAuthTime = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt32(claim.Value));
                        identity.AuthTimeUTC = dateTimeOffsetAuthTime.UtcDateTime;
                        continue;
                    case "exp":
                        DateTimeOffset dateTimeOffsetExpiry = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt32(claim.Value));
                        identity.ExpiryUTC = dateTimeOffsetExpiry.UtcDateTime;
                        continue;
                    case "iss":
                        identity.Issuer = claim.Value;
                        continue;
                    case "http://schemas.microsoft.com/identity/claims/identityprovider":
                        identity.IdentityProvider = claim.Value;
                        continue;
                    case "emails":
                        identity.EmailAddress = claim.Value;
                        continue;
                    case "extension_Email":
                        identity.EmailAddress = claim.Value;
                        continue;
                    case "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress":
                        identity.EmailAddress = claim.Value;
                        continue;
                    case "http://schemas.microsoft.com/identity/claims/objectidentifier":
                    case "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier":
                    case "appid":
                        try
                        {
                            var objectId = Guid.Parse(claim.Value);
                            identity.ObjectId = objectId;
                        }
                        catch (Exception)
                        {

                        }
                        continue;
                    case "company":
                    case "extn.company":
                    case "extension_company":
                        identity.ClaimedCompany = claim.Value;
                        continue;
                    case "misc":
                        identity.Misc = claim.Value;
                        continue;
                    default:
                        continue;
                }
            }

            return identity;
        }

        internal static bool NullOrLessThanOne(decimal? value)
        {
            return (value == null || value < 1);
        }
    }
}