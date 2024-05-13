using _2Sport_BE.API.Services;
using _2Sport_BE.DataContent;
using _2Sport_BE.Infrastructure.Services;
using _2Sport_BE.Repository.Interfaces;
using _2Sport_BE.Service.Services;
using _2Sport_BE.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        [Route("refresh")]
        [HttpPost]
        public async Task<IActionResult> Refresh([FromBody] TokenModel request)
        {
            var result = await _identityService.RefreshTokenAsync(request);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("logout")]
        public async Task<IActionResult> LogOut()
        {
            var currentUserId = GetUserIdFromToken();
            //var user = await _userService.GetAsync(_ => _.Id == currentUserId);
            var token = await _refreshTokenService.GetTokenDetail(currentUserId);
            token.Token = null;
            token.ExpiryDate = DateTime.MinValue;

            await _refreshTokenService.UpdateToken(token);
            _unitOfWork.Save();
            return Ok();
        }
        protected int GetUserIdFromToken()
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
        }
    }
}
