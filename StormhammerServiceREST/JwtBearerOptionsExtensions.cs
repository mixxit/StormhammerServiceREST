using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Security.Claims;


namespace StormhammerServiceREST
{
    public static class JwtBearerOptionsExtensions
    {
        public static void AddAdditionalJWTConfig(this JwtBearerOptions options, bool isApplicationUser, string alertEmail, string azureAdDomain, ILogger logger)
        {
            options.Events = new JwtBearerEvents
            {
                OnTokenValidated = async ctx =>
                {
                    try
                    {
                        //Get the calling app client id that came from the token produced by Azure AD
                        string objectId = ctx.Principal.FindFirstValue("http://schemas.microsoft.com/identity/claims/objectidentifier") ??
                        ctx.Principal.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier") ??
                        ctx.Principal.FindFirstValue("appid");

                        string email = ctx.Principal.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress") ??
                        ctx.Principal.FindFirstValue("emails") ??
                        ctx.Principal.FindFirstValue("extension_Email");


                        var db = ctx.HttpContext.RequestServices.GetRequiredService<StormhammerContext>();
                        if (db.AttemptRegisterUser(ctx.Principal, Guid.Parse(objectId), email) == true)
                        {
                            logger.LogInformation("RegisterUser returned true, sending user joined email - ObjectId: " + objectId);
                        }
                        else
                        {
                            logger.LogInformation("RegisterUser returned false, not sending user joined email - ObjectId: " + objectId);
                        }
                    }
                    catch (Exception e)
                    {
                        // Allow http request to continue even if there is some exception in SQL
                        logger.LogError("Exception in OnTokenValidated:" + e.Message + " " + e.InnerException);
                    }
                }
            };
        }
    }
}
