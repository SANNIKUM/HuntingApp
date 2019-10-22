using AFFHA.API.Application.Helpers;
using AFFHA.API.Application.Requests.Pictures;
using AFFHA.Domain;
using AFFHA.Domain.Pictures;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AFFHA.API.Application.RequestHandlers.Pictures
{
    public class PictureCreateRequestHandler : IRequestHandler<PictureCreateRequest, bool>
    {
        private readonly IPictureRepository _pictureRepository;
        private readonly IMapper _mapper;
        public PictureCreateRequestHandler(IPictureRepository pictureRepository, IMapper mapper)
        {
            _pictureRepository = pictureRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(PictureCreateRequest request, CancellationToken cancellationToken)
        {
            IList<Picture> pictures = new List<Picture>();
            foreach(var file in FilesUploadHelper.GetFilesDetail(request.Pictures, (int)FileType.Picture))
            {
                pictures.Add(new Picture
                {
                    ZoneId = request.ZoneId.Value,
                    Name = file.Name,
                    ContentType= file.ContentType,
                    BinaryData = file.ContentBinary
                });
            }
            if (pictures.Count() > 0)
            {
                _pictureRepository.AddRangeAsync(pictures);
                return await _pictureRepository.UnitOfWork.SaveEntitiesAsync();
            }
              
            throw new Exception("Unabe to add pictures to zone");
        }
    }
}
