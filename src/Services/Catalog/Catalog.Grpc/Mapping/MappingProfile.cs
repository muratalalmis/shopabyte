using AutoMapper;
using Catalog.Domain.Entities;

namespace Catalog.Grpc.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductListModel>();
        }
    }
}
