using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace API;

public static class IdentityServiceExtension{

    /**
    * AddIdentityServices method
    * This method is used to add identity services
    * @param services: The IServiceCollection object
    * @param config: The IConfiguration object
    * @return The IServiceCollection object
    */
    public static IServiceCollection AddIdentityServices(this IServiceCollection services,
    IConfiguration config){
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
            {
                var tokenKey = config["TokenKey"] ?? 
                throw new Exception("Token key not found");
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            }
        );
        return services;
    }
}