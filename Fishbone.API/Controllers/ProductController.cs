using Fishbone.Application.Interfaces;
using Fishbone.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Fishbone.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : BaseController
    {
        private readonly IProductServices productServices;

        public ProductController(IProductServices productServices)
        {
            this.productServices = productServices;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await productServices.Get(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await productServices.GetAll();
            return result == null || result.Count == 0 ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Product model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await productServices.Add(model);
            if (result == null)
            {
                return StatusCode(500, "An error occurred while adding the product.");
            }
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }
    }
}
