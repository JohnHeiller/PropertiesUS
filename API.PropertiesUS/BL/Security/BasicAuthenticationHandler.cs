using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace API.PropertiesUS.Controllers
{
    /// <summary>
    /// Class for basic authentication
    /// </summary>
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        #region Property
        public IConfiguration _configuration { get; }
        private readonly string _username = string.Empty;
        private readonly string _password = string.Empty;
        #endregion

        #region Constructor  
        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="options">IOptionsMonitor(AuthenticationSchemeOptions) data</param>
        /// <param name="logger">ILoggerFactory data</param>
        /// <param name="encoder">UrlEncoder data</param>
        /// <param name="configuration">IConfiguration data</param>
        /// <param name="clock">ISystemClock data</param>
        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            IConfiguration configuration,
            ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
            _configuration = configuration;
            _username = configuration.GetSection("Security")["Username"];
            _password = configuration.GetSection("Security")["Password"];
        }
        #endregion

        /// <summary>
        /// Authentication handling method
        /// </summary>
        /// <returns>Success or Fail</returns>
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            string username;
            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var credentials = Encoding.UTF8.GetString(Convert.FromBase64String(authHeader.Parameter)).Split(':');
                username = credentials.FirstOrDefault();
                var password = credentials.LastOrDefault();

                if (!ValidateCredentials(username, password))
                    throw new ArgumentException("Invalid credentials");
            }
            catch (Exception ex)
            {
                return AuthenticateResult.Fail($"Authentication failed: {ex.Message}");
            }

            var claims = new[] {
                new Claim(ClaimTypes.Name, username)
            };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }

        /// <summary>
        /// Method to validate access credentials
        /// </summary>
        /// <param name="username">username received</param>
        /// <param name="password">password received</param>
        /// <returns></returns>
        private bool ValidateCredentials(string username, string password)
        {
            return username.Equals(_username) && password.Equals(_password);
        }

    }
}
