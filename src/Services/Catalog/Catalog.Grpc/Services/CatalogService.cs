using AutoMapper;
using Catalog.Infrastructure.Repositories;
using Grpc.Core;

namespace Catalog.Grpc.Services
{
    public class CatalogService : CatalogGrpcService.CatalogGrpcServiceBase
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly ILogger<CatalogService> _logger;

        public CatalogService(
            IMapper mapper,
            IProductRepository productRepository,
            ILogger<CatalogService> logger)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _logger = logger;
        }

        public override async Task<ProductListModel> GetProductById(GetDiscountRequest request, ServerCallContext context)
        {
            var product = await _productRepository.Get(request.Id);
            var response = _mapper.Map<ProductListModel>(product);

            return response;
        }
    }
}