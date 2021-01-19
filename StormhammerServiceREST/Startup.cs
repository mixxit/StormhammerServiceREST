using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace StormhammerServiceREST
{
    public class Startup
    {
        public Startup(IConfiguration configuration, ILogger<Startup> logger)
        {
            this.Configuration = configuration;
            this.Logger = logger;
        }

        public IConfiguration Configuration { get; }
        public ILogger<Startup> Logger { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var validAudiences = new List<string> {
                Configuration["AzureAdB2C:ClientId"]
            };

            // DE-1558
            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            // DE-1558
            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            services.AddSingleton<IConfiguration>(Configuration);

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            // order is vital, this *must* be called *after* AddNewtonsoftJson()
            services.AddSwaggerGenNewtonsoftSupport();

            services.AddAuthentication().AddJwtBearer("AzureADB2C", o =>
            {
                o.Authority = $"{Configuration["AzureAdB2C:Instance"]}/{Configuration["AzureAdB2C:TenantId"]}/{Configuration["AzureAdB2C:SignUpSignInPolicyId"]}/v2.0/";

                o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidAudiences = validAudiences
                };

                //Additional config snipped
                o.AddAdditionalJWTConfig(isApplicationUser: false, Configuration["NewUser:EmailAdress"], Configuration["AzureAd:Domain"], this.Logger);
            }).AddJwtBearer("email", o =>
            {
                o.Authority = $"{Configuration["AzureAdB2C:Instance"]}/{Configuration["AzureAdB2C:TenantId"]}/{Configuration["AzureAdB2C:EmailSignUpSignInPolicyId"]}/v2.0/";

                o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidAudiences = validAudiences
                };

                //Additional config snipped
                o.AddAdditionalJWTConfig(isApplicationUser: false, Configuration["NewUser:EmailAdress"], Configuration["AzureAd:Domain"], this.Logger);
            }).AddSteam("steam", o =>
            {
                o.ApplicationKey = "6235A3BC6BB82971121A43157BAECDB1";
                o.SaveTokens = true;
            });

            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .AddAuthenticationSchemes("AzureADB2C","email", "steam")
                .Build();
            });

            services.AddDbContext<StormhammerContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), builder =>
                {
                    builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                }),
                ServiceLifetime.Transient);
            services.AddOptions();

            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "StormhammerServiceREST", Version = "v1" });
                //c.OperationFilter<FileUploadOperationFilter>();
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        Implicit = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri($"{Configuration["AzureAdB2C:LoginRootURL"]}/tfp/{Configuration["AzureAdB2C:TenantId"]}/{Configuration["AzureAdB2C:SignUpSignInPolicyId"]}/oauth2/v2.0/authorize"),
                            TokenUrl = new Uri($"{Configuration["AzureAdB2C:LoginRootURL"]}/tfp/{Configuration["AzureAdB2C:TenantId"]}/{Configuration["AzureAdB2C:SignUpSignInPolicyId"]}/oauth2/v2.0/token"),
                            Scopes = new Dictionary<string, string>
                            {
                                { $"{Configuration["AzureAdB2C:AppIDURL"]}/user_impersonation", "user_impersonation" }
                            }
                        }
                    }
                });

                c.AddSecurityDefinition("email", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        Implicit = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri($"{Configuration["AzureAdB2C:LoginRootURL"]}/tfp/{Configuration["AzureAdB2C:TenantId"]}/{Configuration["AzureAdB2C:EmailSignUpSignInPolicyId"]}/oauth2/v2.0/authorize"),
                            TokenUrl = new Uri($"{Configuration["AzureAdB2C:LoginRootURL"]}/tfp/{Configuration["AzureAdB2C:TenantId"]}/{Configuration["AzureAdB2C:EmailSignUpSignInPolicyId"]}/oauth2/v2.0/token"),
                            Scopes = new Dictionary<string, string>
                            {
                                { $"{Configuration["AzureAdB2C:AppIDURL"]}/user_impersonation", "user_impersonation" }
                            }
                        }
                    }
                });

                c.AddSecurityDefinition("steam", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OpenIdConnect,
                    OpenIdConnectUrl = new Uri("http://steamcommunity.com/openid")
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement{
                    { new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
                        }, new[] { "user_impersonation" }
                    },
                    { new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "email" }
                        }, new[] { "user_impersonation" }
                    },
                    { new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "steam" }
                        }, new string[] { }
                    },
                });

            });

            services.AddMvc().AddNewtonsoftJson(o =>
            {
                o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");
                context.Response.Headers.Add("Content-Security-Policy", "frame-ancestors 'self';");
                await next();
            });

            app.UseCors("CorsPolicy");
            app.UseAuthentication();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.OAuthClientId(Configuration["Swagger:ClientId"]);
                c.OAuthRealm(Configuration["AzureAdB2C:ClientId"]);
                c.OAuthAppName("Stormhammer");
                c.OAuthScopeSeparator(" ");
                c.OAuthAdditionalQueryStringParams(new Dictionary<string, string> { { "resource", Configuration["AzureAdB2C:ClientId"] } });
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Stormhammer");
            });
            app.UseExceptionHandler("/api/Error");

            app.UseRouting();

            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
