using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;
using FraudDetectionCsharp.Domain.interfaces;

namespace FraudDetectionCsharp.Domain.services
{
    public class AuthenticationService : IAuthenticationService
    {
        public async Task<AuthenticationResult> AuthenticateAsync(string username, string password)
        {
            // Implementação do método de autenticação
            if (username == "admin" && password == "password")
            {
                var token = GenerateJwtToken();

                return new AuthenticationResult
                {
                    IsAuthenticated = true,
                    Token = token
                };
            }

            return new AuthenticationResult
            {
                IsAuthenticated = false,
                ErrorMessage = "Invalid username or password"
            };
        }

        private string GenerateJwtToken()
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJleHAiOjE2OTc4OTY0MzIsImlzcyI6Imh0dHA6Ly95b3VyZG9tYWluLmNvbSIsImF1ZCI6Imh0dHA6Ly95b3VyZG9tYWluLmNvbSJ9.jbhq5rs2v-jCN90YX4e8SU_83vHuu6v9kwumpZG8fSo"));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(
                issuer: "http://yourdomain.com",
                audience: "http://yourdomain.com",
                claims: new List<Claim>(),
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: signingCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return tokenString;
        }
    }
}
