using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ProjectAssignment.Models;
using ProjectAssignment.ProductRepo;

namespace ProjectAssignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    { 
        private readonly IProductRepository _prodRepo;
        private readonly ILogger<ProductController> _logger;
        private readonly AppSettings _appSettings;
        
        public ProductController(IProductRepository prodRepo, ILogger<ProductController> logger, IOptions<AppSettings> appSettings)
        {

            _prodRepo = prodRepo;
            _logger = logger;
            _appSettings = appSettings.Value;

        }


        [HttpGet("category/{categoryname}")]
        public async Task<IActionResult> GetProductByCategory(string categoryname)
        {
           
            try
            {
                _logger.LogInformation("Fetching product with category {categoryname}.", categoryname);
                var products = await _prodRepo.GetProductsByCategoryNameAsync(categoryname);
                if (!products.Any())
                {
                    return NotFound(CommonResponse<string>.Fail("$ No Products for  given category "));
                }

                return Ok(CommonResponse<IEnumerable<Products>>.Success(products, "Products fetched successfully."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching product with category {categoryname}.", categoryname);
                return StatusCode(500, CommonResponse<string>.Fail("Internal server error."));
            }


        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string name)
        {
            try
            {
                _logger.LogInformation("Searching  product by {name}.", name);
                var products = await _prodRepo.SearchByNameAsync(name);
                if (!products.Any())
                {
                    return NotFound(CommonResponse<string>.Fail("No products match the search criteria."));
                }
                return Ok(CommonResponse<IEnumerable<Products>>.Success(products, "Search product fetched successfully.")); ;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while Searching  product by {name}.", name);
                return StatusCode(500, CommonResponse<string>.Fail( "Internal server error."));
            }

        }
        [HttpGet("/")]
        public IActionResult GetAppSettings()
        {
            return Ok(new { _appSettings.AppName, _appSettings.FeatureEnabled });
        }

    }
}
