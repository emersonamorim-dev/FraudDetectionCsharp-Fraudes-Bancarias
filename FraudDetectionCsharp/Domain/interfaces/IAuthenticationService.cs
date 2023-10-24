using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraudDetectionCsharp.Domain.interfaces
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResult> AuthenticateAsync(string username, string password);
    }

    public class AuthenticationResult
    {
        public bool IsAuthenticated { get; set; }
        public string Token { get; set; }
        public string ErrorMessage { get; set; }
    }

}
