using System.Collections.Generic;
using System.Threading.Tasks;
using HowToTest.Infrastructure.Models;
using HowToTest.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace HowToTest.Web.Controllers
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
        public async Task<ActionResult<IEnumerable<User>>> GetAllAsync()
        {
            var items = await _userService.GetAllAsync();
            return Ok(items);
        }
    }
}
