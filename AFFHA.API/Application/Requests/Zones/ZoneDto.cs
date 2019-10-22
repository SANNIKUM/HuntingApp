using AFFHA.API.Application.Requests.Pictures;
using System.Collections.Generic;

namespace AFFHA.API.Application.Requests.Zones
{
    public class ZoneDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string JeoData { get; set; }


        public IList<PictureDto> pictures { get; set; }

    }
}
