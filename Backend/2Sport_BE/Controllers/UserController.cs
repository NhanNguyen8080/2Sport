using _2Sport_BE.Repository.Models;
using _2Sport_BE.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            var users = await Task.FromResult(_userService.GetAll().ToList());
            if(users == null)
            {
                return NotFound();
            }
            return users;
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            var employees = await Task.FromResult(_userService.GetUserById(id));
            if (employees == null)
            {
                return NotFound();
            }
            return employees;
        }
        [HttpPost]
        public async Task<ActionResult<User>> AddUser(User user)
        {
            _userService.Add(user);
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
                _userService.Update(user);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_userService.CheckExist(id))
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
            var user = _userService.GetUserById(id);
            if(user == null)
            {
                return NotFound();
            }
            _userService.Remove(id);
            return await Task.FromResult(user);
        }

        [Route("getCurrentUser")]
        [HttpGet]
        [Authorize]
        public IActionResult GetCurrentUser()
        {
            var UserId = GetUserIdFromToken();
            var result = _userService.Get(_ => _.Id == UserId);
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
