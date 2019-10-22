using AFFHA.API.Application.Requests.Zones;
using AFFHA.Domain.Zones;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AFFHA.API.Application.Handlers.Zones
{
    public class ZonesGetRequestHandler : IRequestHandler<ZonesGetRequest, ZoneDto>
    {
        private readonly IZoneRepository _zoneRepository;
        private readonly IMapper _mapper;
        public ZonesGetRequestHandler(IZoneRepository zoneRepository, IMapper mapper)
        {
            _zoneRepository = zoneRepository;
            _mapper = mapper;
        }
        public async Task<ZoneDto> Handle(ZonesGetRequest request, CancellationToken cancellationToken)
        {
            var zone = await _zoneRepository.GetAsync(request.Id);
            return _mapper.Map<ZoneDto>(zone);
        }

       
    }
}
