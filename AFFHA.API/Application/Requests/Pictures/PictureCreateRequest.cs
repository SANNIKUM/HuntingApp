using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace AFFHA.API.Application.Requests.Pictures
{
    public class PictureCreateRequest : IRequest<bool>
    {
        public int? ZoneId { get; set; }
        public IList<IFormFile> Pictures { get; set; }

        public PictureCreateRequest(int ZoneId, IList<IFormFile> Pictures)
        {
            this.ZoneId = ZoneId;
            this.Pictures = Pictures;
        }

    }

    public class PicturesRequest
    {
        [FromForm]
        public IList<IFormFile> Pictures { get; set; }
    }
}
