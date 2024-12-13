using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.IRepository;
using WebAPI.Repository;
using WebData.Model;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IRepProduct _IrepProduct;
        public ProductController(IRepProduct repProduct)
        {
            _IrepProduct = repProduct;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var Products = await _IrepProduct.GetAll();
            return Ok(Products);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] Product product, IFormFile file, [FromForm] List<int> categoryIds)
        {
            try
            {
                await _IrepProduct.Create(product, file, categoryIds);
                return Ok(new { message = "Product created successfully" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromForm] Product product, IFormFile file, [FromForm] List<int> categoryIds)
        {
            try
            {
                await _IrepProduct.Update(product, file, categoryIds, id);
                return Ok(new { message = "Product created successfully" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _IrepProduct.Delete(id);
            return Ok(new { message = "Delete Successfully" });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var GetById = await _IrepProduct.GetById(id);
            return Ok(GetById);
        }
    }
}
