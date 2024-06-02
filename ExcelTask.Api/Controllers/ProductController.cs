using ExcelTask.Core.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace ExcelTask.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;
        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpPost("upload-products")]
        public async Task<IActionResult> UploadProducts(IFormFile file)
        {
            try
            {
                await _productService.CreateProductsFromXlsx(file);
                return Ok();
            }
            catch (Exception ex)
            {
                var errMessage = $"Error while uploading products: {ex.Message}";
                _logger.LogError(errMessage);
                return BadRequest(errMessage);
            }
        }
    }
}
