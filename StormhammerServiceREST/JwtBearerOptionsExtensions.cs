using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StormhammerServiceREST
{
    public static class JwtBearerOptionsExtensions
    {
        public static void AddAdditionalJWTConfig(this JwtBearerOptions options, ILogger logger)
        {
            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    var accessToken = context.Request.Query["access_token"];

                    // If the request is for our hub...
                    var path = context.HttpContext.Request.Path;
                    if (!string.IsNullOrEmpty(accessToken) 
                        && path.StartsWithSegments("/worldhub") 
                        //&& path.StartsWithSegments("/identityhub") 
                        )
                    {
                        // Read the token out of the query string
                        context.Token = accessToken;
                    }
                    return Task.CompletedTask;
                },
                OnTokenValidated = async ctx =>
                {
                    try
                    {
                        //Get the calling app client id that came from the token produced by Azure AD
                        string objectId = ctx.Principal.FindFirstValue("http://schemas.microsoft.com/identity/claims/objectidentifier") ??
                        ctx.Principal.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier") ??
                        ctx.Principal.FindFirstValue("appid") ??
                        ctx.Principal.FindFirstValue("UserId");

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
