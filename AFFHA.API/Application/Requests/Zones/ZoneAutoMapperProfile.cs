using AFFHA.Domain;
using AutoMapper;

namespace AFFHA.API.Application.Requests.Zones
{
    public class PictureAutoMapperProfile : Profile
    {
        public PictureAutoMapperProfile()
        {
            CreateMap<Zone, ZoneDto>();
            CreateMap<ZoneCreateRequest, Zone>();
        }
    }
}
