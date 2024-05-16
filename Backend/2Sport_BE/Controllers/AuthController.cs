using _2Sport_BE.API.Services;
using _2Sport_BE.DataContent;
using _2Sport_BE.Infrastructure.Services;
using _2Sport_BE.Repository.Interfaces;
using _2Sport_BE.Service.Services;
using _2Sport_BE.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Linq;

namespace _2Sport_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public readonly IUserService _userService;
        private readonly IIdentityService _identityService;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly IUnitOfWork _unitOfWork;
        public AuthController(
            IUserService userService,
            IIdentityService identityService,
            IRefreshTokenService refreshTokenService,
            IUnitOfWork unitOfWork)
        {
            _userService = userService;
            _identityService = identityService;
            _refreshTokenService = refreshTokenService;
            _unitOfWork = unitOfWork;
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromBody] UserLogin loginModel)
        {
            var result = await _identityService.LoginAsync(loginModel);
            return Ok(result);
        }

        [Route("refresh-token")]
        [HttpPost]
        public async Task<IActionResult> Refresh([FromBody] TokenModel request)
        {
            var result = await _identityService.RefreshTokenAsync(request);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("logout")]
        public async Task<IActionResult> LogOut([FromBody] TokenModel request)
        {
            var token = await _unitOfWork.RefreshTokenRepository.GetObjectAsync(_ => _.Token == request.Token);
            if (token == null)
            {
                return BadRequest();
            }
            else
            {
                token.Token = null;
                token.ExpiryDate = DateTime.MinValue;

                await _refreshTokenService.UpdateToken(token);
                _unitOfWork.Save();
                return Ok();
            }
        }
        [HttpGet("oauth-login")]
        public IActionResult ExternalLogin1()
        {
            var props = new AuthenticationProperties { RedirectUri = "Auth/signin-google" };
            return Challenge(props, GoogleDefaults.AuthenticationScheme);
        }
        [HttpGet("signin-google")]
        public async Task<IActionResult> GoogleLogin()
        {
            var response = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (response.Principal == null) return BadRequest();

            var name = response.Principal.FindFirstValue(ClaimTypes.Name);
            var givenName = response.Principal.FindFirstValue(ClaimTypes.GivenName);
            var email = response.Principal.FindFirstValue(ClaimTypes.Email);
            
            //Do something with the claims
            // var user = await UserService.FindOrCreate(new { name, givenName, email});

            return Ok();
        }
        /*protected int GetUserIdFromToken()
        {
            int UserId = 0;
            try
            {
                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    var identity = HttpContext.User.Identity as ClaimsIdentity;
                    if (identity != null)
                    {
                        IEnumerable<Claim> claims = identity.Claims;
                        string strUserId = identity.FindFirst("UserId").Value;
                        int.TryParse(strUserId, out UserId);

                    }
                }
                return UserId;
            }
            catch
            {
                return UserId;
            }
        }*/
    }
}
