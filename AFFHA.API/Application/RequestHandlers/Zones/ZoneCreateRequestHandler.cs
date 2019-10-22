using AFFHA.API.Application.Requests.Zones;
using AFFHA.Domain;
using AFFHA.Domain.Zones;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AFFHA.API.Application.RequestHandlers.Zones
{
    public class ZoneCreateRequestHandler : IRequestHandler<ZoneCreateRequest, bool>
    {
        private readonly IZoneRepository _zoneRepository;
        private readonly IMapper _mapper;
        public ZoneCreateRequestHandler(IZoneRepository zoneRepository, IMapper mapper)
        {
            _zoneRepository = zoneRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(ZoneCreateRequest request, CancellationToken cancellationToken)
        {
            var zone = _mapper.Map<Zone>(request);
            _zoneRepository.AddAsync(zone);
            
            return await _zoneRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
