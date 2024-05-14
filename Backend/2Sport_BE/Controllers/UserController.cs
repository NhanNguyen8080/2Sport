using _2Sport_BE.Repository.Models;
using _2Sport_BE.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using _2Sport_BE.DataContent;
using _2Sport_BE.ViewModels;
using AutoMapper;
using _2Sport_BE.Service.Services;
using System.Text;
using System.Security.Cryptography;

namespace _2Sport_BE.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IRefreshTokenService _refreshTokenService;
        public UserController(
            IUserService userService,
            IMapper mapper,
            IRefreshTokenService refreshTokenService
            )
        {
            _userService = userService;
            _mapper = mapper;
            _refreshTokenService = refreshTokenService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            ResponseModel<User> response = new ResponseModel<User>();
            var users = await _userService.GetAllAsync();
            if(users == null)
            {
                return NotFound();
            }
            response.IsSuccess = true;
            response.Message = "Query Successfully";

            foreach (var user in users)
            {
                
            }
            return Ok();
        }
        
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserDetail(int userId)
        {
            try
            {
                var user = await _userService.FindAsync(userId);
                var tokenUser = await _refreshTokenService.GetTokenDetail(userId);
                return Ok(new { User = user, Token = tokenUser });
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUser([FromBody] User staff)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _userService.AddAsync(staff);
                _userService.Save();
                return StatusCode(201, new { processStatus = "Success", user = staff }); ;
            }
            catch (Exception ex)
            {
                //Duplicate
                if (ex is DbUpdateException dbUpdateEx)
                {
                    return BadRequest(new { processStatus = "Duplicate" });
                }
                return BadRequest(ex);
            }

        }
        [HttpPost("sign-up")]
        public async Task<IActionResult> CreateUser([FromBody] UserCM userCM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var user = _mapper.Map<UserCM, User>(userCM);
                user.Password = HashPassword(userCM.Password);
                user.CreatedDate = DateTime.Now;
                user.RoleId = 1;
                user.IsActive = true;
                await _userService.AddAsync(user);
                _userService.Save();
                return StatusCode(201, new { processStatus = "Success", user = user }); ;
            }
            catch(Exception ex)
            {
                //Duplicate
                if (ex is DbUpdateException dbUpdateEx)
                {
                    return BadRequest(new { processStatus = "Duplicate" });
                }
                return BadRequest(ex);
            }
            
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<User>> UpdateUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != user.Id)
            {
                return BadRequest();
            }
            try
            {
               await _userService.UpdateAsync(user);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (! await _userService.CheckExistAsync(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return await Task.FromResult(user);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var user = await _userService.FindAsync(id);
            if(user == null)
            {
                return NotFound();
            }
            await _userService.RemoveAsync(id);
            return await Task.FromResult(user);
        }
        [HttpPut("change-status/{id}")]
        public async Task<IActionResult> ChangeStatusUser(int id)
        {
            try
            {
                var user = await _userService.FindAsync(id);
                if (user == null)
                {
                    return BadRequest(new { processStatus = "Not Existed" });
                }
                user.IsActive = !user.IsActive;
                await _userService.UpdateAsync(user);
                _userService.Save();
                return Ok(new { processStatus = "Success", data = id });
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [Route("getCurrentUser")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
            var UserId = GetUserIdFromToken();
            var result = await _userService.GetAsync(_ => _.Id == UserId);
            return Ok(result);
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
