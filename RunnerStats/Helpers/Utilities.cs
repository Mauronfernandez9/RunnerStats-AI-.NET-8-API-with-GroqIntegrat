using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using RunnerStats.Models;
using RunnerStats.Models.Entities;

namespace RunnerStats.Helpers
{
    public class Utilities
    {

        private readonly IConfiguration _configuration;

        public Utilities(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string EncryptSha256(string text)
        {
            using (SHA256 sha256Hash = SHA256.Create()) 
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(text));
                StringBuilder builder = new StringBuilder();
                for(int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();

            }
        }

        public string GenerateJWT(User Model)
        {
            var UserClaims = new[]
            {
                new Claim(ClaimTypes.Email,Model.Email),
                new Claim("UserId",Model.IdUser.ToString()),
                new Claim("RunnerId",Model.RunnerId.ToString())

            };


            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var jwtConfig = new JwtSecurityToken(claims: UserClaims,expires: DateTime.UtcNow.AddMinutes(10),signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(jwtConfig);
        
        }











    }

    
    



}
