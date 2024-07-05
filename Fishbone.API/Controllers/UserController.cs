using Fishbone.Application.Interfaces;
using Fishbone.Core;
using Fishbone.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Fishbone.API.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class UserController : ControllerBase
    {
        private readonly IUserServices userServices;
        private readonly OrderDbContext orderDbContext;

        public UserController(IUserServices userServices, OrderDbContext orderDbContext)
        {
            this.userServices = userServices;
            this.orderDbContext = orderDbContext;
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> Get(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return BadRequest("Invalid username");
            }
            var result = await userServices.Get(username);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int page = 1, int size = 5)
        {
            if (page <= 0 || size <= 0)
            {
                return BadRequest("Invalid page or size values");
            }
            var result = await userServices.GetAll(page, size);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(User model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await userServices.Add(model);
            return Ok(result);
        }

        [HttpPost("edit")]
        public async Task<IActionResult> Edit(UserUpdateDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await userServices.Update(model);
            return Ok(result);
        }
    }
}
