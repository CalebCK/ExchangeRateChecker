using CurrencyConvertor.V1.DataTransferObjects;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UCMSAPI.Constants;

namespace CurrencyConvertor.V1.Services
{
    public partial class UserService
    {
        private bool userExists(string username)
        {
            var foundUser = _userManager.FindByNameAsync(username);
            return foundUser.Result != null;
        }

        private bool userEmailExists(string email)
        {
            var foundUser = _userManager.FindByEmailAsync(email);
            return foundUser.Result != null;
        }

        private async Task<LoginResponseDto> GenerateTokenObject(IdentityUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(AuthConstants.TokenLifeSpan)).ToUnixTimeSeconds().ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtTokenSettings.JwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_jwtTokenSettings.JwtExpireDays));

            var token = new JwtSecurityToken(
                _jwtTokenSettings.JwtIssuer,
                _jwtTokenSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: creds
            );

            var tokenStr = new JwtSecurityTokenHandler().WriteToken(token);
            var refreshToken = await GenerateRefreshTokenAsync(user);

            await SaveRefreshTokenAsync(user, refreshToken);

            LoginResponseDto output = new LoginResponseDto
            {
                TokenType = "Bearer",
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                //RefreshToken = refreshToken,
            };

            return output;
        }

        private async Task<string> GenerateRefreshTokenAsync(IdentityUser user)
        {
            var token = await _userManager.GenerateUserTokenAsync(user, AuthConstants.TokenProvider, AuthConstants.TokenPurpose);
            return token;
        }

        private async Task SaveRefreshTokenAsync(IdentityUser user, string refreshToken)
        {
            var result = await _userManager.SetAuthenticationTokenAsync(user, AuthConstants.TokenProvider, AuthConstants.TokenPurpose, refreshToken);
            if (!result.Succeeded)
            {
                throw new Exception("Failed to save refresh token");
            }
        }
    }
}
