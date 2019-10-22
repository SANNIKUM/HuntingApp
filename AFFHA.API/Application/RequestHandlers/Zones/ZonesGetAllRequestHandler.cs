


using AFFHA.API.Application.Requests.Zones;
using AFFHA.Domain.Zones;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AFFHA.API.Application.Handlers.Zones
{
    public class ZonesGetAllRequestHandler : IRequestHandler <ZonesGetAllRequest, IEnumerable<ZoneDto>>
    {
        private readonly IZoneRepository _zoneRepository;
        private readonly IMapper _mapper;
        public ZonesGetAllRequestHandler(IZoneRepository zoneRepository, IMapper mapper)
        {
            _zoneRepository = zoneRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ZoneDto>> Handle(ZonesGetAllRequest request, CancellationToken cancellationToken)
        {
            var zones =  await _zoneRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ZoneDto>>(zones); ;
        }
    }
}
