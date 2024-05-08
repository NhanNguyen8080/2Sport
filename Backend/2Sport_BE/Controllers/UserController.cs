using _2Sport_BE.Repository.Models;
using _2Sport_BE.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using _2Sport_BE.DataContent;

namespace _2Sport_BE.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
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
        
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            var employees = await _userService.GetUserByIdAsync(id);
            if (employees == null)
            {
                return NotFound();
            }
            return employees;
        }
        [HttpPost]
        public async Task<ActionResult<User>> AddUser(User user)
        {
            await _userService.AddAsync(user);
            return await Task.FromResult(user);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<User>> UpdateUser(int id, User user)
        {
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
            var user = await _userService.GetUserByIdAsync(id);
            if(user == null)
            {
                return NotFound();
            }
            await _userService.RemoveAsync(id);
            return await Task.FromResult(user);
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
    }
}
