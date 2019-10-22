using MediatR;
using System.Collections.Generic;

namespace AFFHA.API.Application.Requests.Zones
{
    public class ZonesGetAllRequest : IRequest<IEnumerable<ZoneDto>>
    {
        public ZonesGetAllRequest()
        {

        }
    }
}
