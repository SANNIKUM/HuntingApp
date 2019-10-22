using AFFHA.API.Application.Requests.Pictures;
using AFFHA.API.Application.Requests.Zones;
using AFFHA.API.Infrastructure.Shared;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AFFHA.API.Controllers
{
    /// <summary>
    /// Zone Controller
    /// </summary>
    /// <seealso cref="Controller" />
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ZonesController : MediatorController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ZonesController"/> class
        /// </summary>
       
        public ZonesController(IMediator mediator, IMapper mapper, ILogger<MediatorController> logger) : base(mediator, mapper, logger)
        {
           
        }

        /// <summary>
        /// Gets hunting areas by Id.
        /// </summary>
        /// <remarks>
        /// Sample requests:
        ///
        ///     GET /api/v1/Zones/
        /// </remarks>
        /// <response code="200">Returns a hunting area with detail</response>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(IEnumerable<ZoneDto>), 200)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetZoneDetail(int id)
        {
            var request = new ZonesGetRequest(id);
            return await HandleRequestAsync(request);
        }

        /// <summary>
        /// Gets a list of all hunting areas.
        /// </summary>
        /// <remarks>
        /// Sample requests:
        ///
        ///     GET /api/v1/Zones/1
        /// </remarks>
        /// <response code="200">Returns an array of areas</response>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ZoneDto>), 200)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllZones(ZonesGetAllRequest request)
        {

            return await HandleRequestAsync(request);
        }

        /// <summary>
        /// Create new hunting area
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<ZoneDto>), 200)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateZone([FromBody] ZoneCreateRequest request)
        {
            
            return await HandleRequestAsync(request);

        }

        /// <summary>
        /// Post pictures to zone
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("{id:int}/pictures")]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(typeof(IEnumerable<PictureDto>), 200)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddPictureToZone(int id, [FromForm] PicturesRequest picturesRequest)
        {
           
            if (picturesRequest == null)
            {
                return BadRequest();
            }
            var request = new PictureCreateRequest(id, picturesRequest.Pictures);

            return await HandleRequestAsync(request);
        }
    }
}