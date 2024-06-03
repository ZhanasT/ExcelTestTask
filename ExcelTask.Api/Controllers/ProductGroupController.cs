using ExcelTask.Core.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExcelTask.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductGroupController : ControllerBase
    {
        private readonly IProductGroupService _productGroupService;
        private readonly ILogger<ProductGroupController> _logger;
        public ProductGroupController(IProductGroupService productGroupService, ILogger<ProductGroupController> logger)
        {
            _productGroupService = productGroupService;
            _logger = logger;
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get() 
        {
            try
            {
                var result = await _productGroupService.GetProductGroupModels();
                return Ok(result);
            }
            catch (Exception ex)
            {
                var errMessage = $"Error while getting all product groups: {ex.Message}";
                _logger.LogError(errMessage);
                return BadRequest(errMessage);
            }
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _productGroupService.GetProductGroupModelById(id);

                if (result == null)
                {
                    throw new Exception($"No group with Id: {id}");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                var errMessage = $"Error while getting all product groups: {ex.Message}";
                _logger.LogError(errMessage);
                return BadRequest(errMessage);
            }
        }
    }
}
