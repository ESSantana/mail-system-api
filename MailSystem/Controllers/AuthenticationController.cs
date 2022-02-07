using MailSystem.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MailSystem.API.Controllers
{
    /// <summary>
    /// Endpoints in charge of handling authentication request
    /// </summary>
    [Route("api/v1/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger">Logger instance</param>
        public AuthenticationController(ILogger<AuthenticationController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Get token to access others endpoints
        /// </summary>
        /// <returns>Return object with auth info</returns>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult<User> Authenticate()
        {
            _logger.LogDebug("Authenticate");

            var mockUser = new User { Name = "SuperUser", Role = "Administrator" };
            var token = TokenHandler.GenerateToken(mockUser);
            mockUser.Token = token;

            _logger.LogDebug("Token value: " + token);

            return mockUser;
        }

        /// <summary>
        /// Check if your token is valid
        /// </summary>
        /// <returns>Returns string if is valid</returns>
        [HttpGet]
        [Route("validateToken")]
        [Authorize()]
        public ActionResult<string> IsTokenValide() => "Valid Token!";
    }
}
