using Fishbone.Application.Interfaces;
using Fishbone.Core.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Fishbone.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderServices orderServices;

        public OrderController(IOrderServices orderServices)
        {
            this.orderServices = orderServices;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0) return BadRequest("Invalid ID");
            var result = await orderServices.Get(id);
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

            var result = await orderServices.GetAll(page, size);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add(OrderModifiedDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await orderServices.Add(model);
            return Ok(result);

        }

        [HttpPost("edit")]
        public async Task<IActionResult> Edit(OrderModifiedDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await orderServices.Update(model);
            return Ok(result);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid ID");
            }
            var result = await orderServices.Delete(id);
            return Ok(result);
        }
    }
}
