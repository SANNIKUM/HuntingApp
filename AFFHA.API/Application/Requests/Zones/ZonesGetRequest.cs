using MediatR;
using System.Collections.Generic;

namespace AFFHA.API.Application.Requests.Zones
{
    public class ZonesGetRequest : IRequest<ZoneDto>
    {
        public int Id { get; set; }
        public ZonesGetRequest(int Id)
        {
            this.Id = Id;
        }
    }
}
