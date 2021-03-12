using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StudentsAPI.Data.Security;
using StudentsAPI.Models.Security;
using StudentsAPI.Services.Security.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace StudentsAPI.Services.Security.Implementacions
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly IConfiguration Configuration;
        private readonly UserManager<User> UserManager;
        public AuthenticateService(UserManager<User> userManager, IConfiguration configuration)
        {
            UserManager = userManager;
            Configuration = configuration;
        }

        public async Task<TokenModel> Register(UserModel user)
        {
            try
            {
                var token = new TokenModel();
                var userLogin = new User()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                };
                var result = await UserManager.CreateAsync(userLogin, user.Password);
                if (!result.Succeeded)
                {
                    token.Succeeded = false;
                    token.Errors = result.Errors.Select(err => (err.Code, err.Description));
                    return token;
                }
                token.Token = GenerateJWT(userLogin, new List<string>());
                token.Succeeded = true;

                return token;
            }
            catch
            {
                throw;
            }
        }

        public async Task<TokenModel> Login(UserModel user)
        {
            try
            {
                var token = new TokenModel();
                var userLogin = await UserManager.FindByNameAsync(user.Email);
                if (user != null && await UserManager.CheckPasswordAsync(userLogin, user.Password))
                {
                    var userRoles = await UserManager.GetRolesAsync(userLogin);
                    token.Token = GenerateJWT(userLogin, userRoles);
                    token.Succeeded = true;
                    return token;
                }
                return token;
            }
            catch
            {
                throw;
            }
        }

        private string GenerateJWT(User user, IList<string> userRoles)
        {
            var authClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
            };
            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtConfig:Secret"]));
            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
             );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
