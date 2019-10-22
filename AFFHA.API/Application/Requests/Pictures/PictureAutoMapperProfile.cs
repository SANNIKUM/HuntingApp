using AFFHA.Domain;
using AutoMapper;

namespace AFFHA.API.Application.Requests.Pictures
{
    public class PictureAutoMapperProfile : Profile
    {
        public PictureAutoMapperProfile()
        {
            CreateMap<Picture, PictureDto>();
        }
    }
}
