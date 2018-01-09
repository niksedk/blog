using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SubIt.Data.Security;

namespace SubIt.Features.Security
{
    public class Jwt : IJwt
    {
        private readonly IConfiguration _config;

        public Jwt(IConfiguration config)
        {
            _config = config;
        }

        public string  GenerateJsonWebToken(SubItUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            foreach (var claim in user?.Claims)
            {
                claims.Add(new Claim(claim.Key, claim.Value));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Token:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Token:Issuer"],
                                             _config["Token:Issuer"],
                                             claims,
                                             expires: DateTime.Now.AddMinutes(120),
                                             signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
