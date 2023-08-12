using System;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;

namespace Async2.Models.Services
{
	public class JwtTokenService
	{

        private IConfiguration configuration;
        private SignInManager<ApplicationUser> signInManager;


        //Initializes the JwtTokenService with IConfiguration and SignInManager.

        public JwtTokenService(IConfiguration config, SignInManager<ApplicationUser> manager)
        {
            configuration = config;
            signInManager = manager;
        }



        // GetValidationPerameters: Returns TokenValidationParameters for JWT token validation.
        // It sets the validation options, such as whether to validate the issuer and audience.

        public static TokenValidationParameters GetValidationPerameters(IConfiguration configuration)
        {
            return new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = GetSecurityKey(configuration),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        }



        // Retrieves the secret key from the configuration and converts it to a SecurityKey.
        // It's used for signing and verifying JWT tokens.

        private static SecurityKey GetSecurityKey(IConfiguration configuration)
        {
            var secret = configuration["JWT:Secret"];
            if (secret == null)
            {
                throw new InvalidOperationException("JWT: Secret key is not exist");
            }

            var secretBytes = Encoding.UTF8.GetBytes(secret);

            return new SymmetricSecurityKey(secretBytes);
        }



        //Generate token to a given user with an expire date
        /* It creates a new JwtSecurityToken and sets its properties, including expiration time, 
         signing credentials, claims based on the user's information.*/

        public async Task<string> GetToken(ApplicationUser user, TimeSpan expiresIn)
        {
            var principle = await signInManager.CreateUserPrincipalAsync(user);

            if (principle == null)
            {
                return null;
            }

            var signingKey = GetSecurityKey(configuration);

            var token = new JwtSecurityToken(
                expires: DateTime.UtcNow + expiresIn,
                signingCredentials: new SigningCredentials(signingKey,
                SecurityAlgorithms.HmacSha256),
                claims: principle.Claims

                );

            return new JwtSecurityTokenHandler().WriteToken(token);


        }


    }
}

/*
 
 signInManager: This is an instance of SignInManager<ApplicationUser>,
 which is a part of ASP.NET Identity framework.
 It provides methods to manage user authentication operations,
 including creating claims principals.
 
  A claims principal represents the identity of the user, and it contains various claims about the user,
  such as their username, roles, and other relevant information.
 */