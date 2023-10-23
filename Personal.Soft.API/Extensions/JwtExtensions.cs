using Amazon.Util;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using MongoDB.Driver;
using Personal.Soft.Infraestructure.Settings;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Personal.Soft.API.Settings;

namespace Personal.Soft.API.Extensions
{
      public static class Extensions
        {
            public static IServiceCollection AddJWT(this IServiceCollection services)
            {
            var configuration = services.BuildServiceProvider().GetService<IConfiguration>()!;
            JwtSettings jwtSettings = configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>()!;

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    // The first one is for our custom jwt
                    .AddJwtBearer(o =>
                    {
                       
                        o.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidIssuer = jwtSettings.Issuer,
                            ValidateIssuer = true,
                            ValidAudience = jwtSettings.Audience,
                            ValidateAudience = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
                            ValidateIssuerSigningKey = true
                        };
                  });

                return services;
            }

        }
    
}
