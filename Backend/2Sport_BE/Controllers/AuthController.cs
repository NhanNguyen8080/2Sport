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
using _2Sport_BE.Repository.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Text;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using _2Sport_BE.Services;

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
        private readonly IMapper _mapper;
        public AuthController(
            IUserService userService,
            IIdentityService identityService,
            IRefreshTokenService refreshTokenService,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _userService = userService;
            _identityService = identityService;
            _refreshTokenService = refreshTokenService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [Route("sign-in")]
        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromBody] UserLogin loginModel)
        {
            var password = HashPassword(loginModel.Password);
            loginModel.Password = password;
            var result = await _identityService.LoginAsync(loginModel);
            return Ok(result);
        }

        [Route("refresh-token")]
        [HttpPost]
        public async Task<IActionResult> RefreshAsync([FromBody] TokenModel request)
        {
            var result = await _identityService.RefreshTokenAsync(request);
            return Ok(result);
        }

        [HttpPost("sign-out")]
        public async Task<IActionResult> LogOutAsync([FromBody] TokenModel request)
        {
            var token = await _unitOfWork.RefreshTokenRepository.GetObjectAsync(_ => _.Token == request.RefreshToken);
            if (token == null)
            {
                return BadRequest("Not found token!");
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
            var props = new AuthenticationProperties { RedirectUri = "api/Auth/signin-google" };
            return Challenge(props, GoogleDefaults.AuthenticationScheme);
        }
        [HttpGet("signin-google")]
        public async Task<IActionResult> GoogleLogin()
        {
            var response = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (response.Principal == null) return BadRequest();

            var name = response.Principal.FindFirstValue(ClaimTypes.Name);
            var email = response.Principal.FindFirstValue(ClaimTypes.Email);
            var phone = response.Principal.FindFirstValue(ClaimTypes.MobilePhone);
            var gender = response.Principal.FindFirstValue(ClaimTypes.Gender);

            if (email == null)
            {
                return BadRequest("Error retrieving Google user information");
            }
            ResponseModel<TokenModel> result = new ResponseModel<TokenModel>();
            var user = await _unitOfWork.UserRepository.GetObjectAsync(_ => _.Email == email);
            if (user != null)
            {
                result = await _identityService.LoginGoogleAsync(user);
                return Ok(result);
            }
            else
            {
                user = new User()
                {
                    FullName = name,
                    Email = email,
                    Phone = phone,
                    CreatedDate = DateTime.Now,
                    RoleId = 4,
                    Gender = gender,
                    IsActive = true,
                };
                await _userService.AddAsync(user);
                _unitOfWork.Save();

                result = await _identityService.LoginGoogleAsync(user);
                return Ok(result);
            }
        }
        [HttpPost("sign-up")]
        public async Task<IActionResult> CreateUser([FromBody] UserCM userCM)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (await _unitOfWork.UserRepository.GetObjectAsync(_ => _.Email.ToLower() == userCM.Email.ToLower()) != null)
                {
                    return BadRequest(new { processStatus = "Email is duplicated" });
                }

                var user = _mapper.Map<UserCM, User>(userCM); 
                user.Password = HashPassword(userCM.Password);
                user.CreatedDate = DateTime.Now;
                user.RoleId = 4;
                user.IsActive = true;
                await _userService.AddAsync(user);
                _userService.Save();
                return StatusCode(201, new { processStatus = "Success", userId = user.Id }); ;
            }
            catch (Exception ex)
            {
                if (ex is DbUpdateException dbUpdateEx)
                {
                    return BadRequest(new { processStatus = "User is duplicated" });
                }
                return BadRequest(ex);
            }

        }
        [HttpPost("forgot-password")]
        //con forgot
        public async Task<IActionResult> ForgotPasswordAsync([FromBody] ForgotVM forgotVM)
        {
            try
            {
                var username = forgotVM.Username;
                var mail = forgotVM.Email;
                //check
                var check = await _unitOfWork.UserRepository.GetObjectAsync(_ => _.Email == mail && _.UserName == _.UserName);
                if(check != null)
                {
                    //Gui mail xac nhan
                    MailRequest mailRequest = new MailRequest();
                    mailRequest.Subject = mail;
                    mailRequest.Body = "";
                    mailRequest.ToEmail = mail;

                }
                else
                {
                    return BadRequest(new { Message = "Invalid Username Or Email!", IsSuccess = false });
                }
                
            }catch(Exception ex)
            {

            }
            return Ok();
        }
        [NonAction]
        public string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
