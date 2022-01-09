using AutoMapper;
using MediatR;
using Ordering.Application.Contracts.Persistence;

namespace Ordering.Application.Features.Sales.Queries.GetSalesByDate
{
    public class GetSalesByDateQueryHandler : IRequestHandler<GetSalesByDateQuery, List<SalesDto>>
    {
        private readonly ISalesRepository _salesRepository;
        private readonly IMapper _mapper;

        public GetSalesByDateQueryHandler(
            ISalesRepository salesRepository,
            IMapper mapper)
        {
            _salesRepository = salesRepository;
            _mapper = mapper;
        }

        public async Task<List<SalesDto>> Handle(GetSalesByDateQuery request, CancellationToken cancellationToken)
        {
            var result = await _salesRepository.GetByDateBetween(request.StartDate, request.EndDate);
            var response = _mapper.Map<List<SalesDto>>(result);

            return response;
        }
    }
}
