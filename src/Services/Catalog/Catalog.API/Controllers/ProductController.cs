using Catalog.Domain.Entities;
using Catalog.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductRepository _productRepository;

        public ProductController(
            ILogger<ProductController> logger,
            IProductRepository productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
        }

        [HttpGet("{id:length(24)}", Name = "GetProduct")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await _productRepository.Get(id);

            if (product == null)
            {
                _logger.LogError($"Product {id} not found.");
                return NotFound();
            }

            return Ok(product);
        }
    }
}
