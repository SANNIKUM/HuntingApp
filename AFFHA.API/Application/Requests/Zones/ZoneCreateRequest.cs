using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AFFHA.API.Application.Requests.Zones
{
    public class ZoneCreateRequest : IRequest<bool>
    {

        public string Name { get; set; }
        public string JeoData { get; set; }

    }
    
}
