using AFFHA.API.Infrastructure.Shared.Errors;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using FluentValidation;
using System.Linq;
using System.Threading.Tasks;
using AFFHA.API.Infrastructure.Shared.Exceptions;
using Microsoft.Extensions.Logging;

namespace AFFHA.API.Infrastructure.Shared
{
    public abstract class MediatorController : ControllerBase
    {
        protected readonly IMediator _mediator;
        protected readonly IMapper _mapper;
        private readonly ILogger<MediatorController> _logger;
        BadRequestErrorResponse error = new BadRequestErrorResponse("The body of the request is empty.");
        public MediatorController(IMediator mediator, IMapper mapper, ILogger<MediatorController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator)); 
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger)); ;
        }

        /// <summary>
        /// When the request's return type and API's return type are same.
        /// </summary>
        /// <param name="request"></param>
        /// <typeparam name="TResponse"></typeparam>
        protected async Task<IActionResult> HandleRequestAsync<TResponse>(IRequest<TResponse> request)
        {
                _logger.LogInformation(
              "----- Sending command: {CommandName}",
              (request.GetType().Name
             ));
            if (request == null)
            {
                    _logger.LogInformation(
             "----- Error in Request: {CommandName}",
             (request.GetType().Name));
                return BadRequest(error);
            }

            return await ExecuteAsync(async () =>
            {
                var response = await _mediator.Send(request);
                return Ok(response);
            });
        }

        /// <summary>
        /// If you want to return different type for API
        /// than the one from MediatR'sIRequest,
        /// provide the second typeparam for AutoMapper.
        /// </summary>
        /// <param name="request"></param>
        /// <typeparam name="TResponse">type of IRequest's response</typeparam>
        /// <typeparam name="TResponseDTO">type of API response</typeparam>
        protected async Task<IActionResult> HandleRequestAsync<TResponse, TResponseDTO>(IRequest<TResponse> request)
        {
            return await ExecuteAsync(async () =>
            {
                if (request == null)
                {
                    return BadRequest(error);
                }

                var response = await _mediator.Send(request);
                var responseDTO = _mapper.Map<TResponseDTO>(response);

                return Ok(responseDTO);
            });
        }

        protected async Task<IActionResult> ExecuteAsync(Func<Task<IActionResult>> action)
        {
            try
            {
                return await action();
            }
            catch (InvalidLoginCredentialsException ex)
            {
                // Right now, "Unauthorized" doesn't accept any body object.
                // Thus, returning "BadRequest" with correct error model.
                var error = new UnauthorizedErrorResponse(ex.Message);
                return BadRequest(error);

                // return Unauthorized();
            }
            catch (ValidationException ex)
            {
                var error = new BadRequestErrorResponse("Validation failed. See 'validationErrors' for details.");
                error.Error.ValidationErrors = ex.Errors.Select(e => e.ErrorMessage).ToArray();
                return BadRequest(error);
            }
            catch (InvalidRequestException ex)
            {
                var error = new BadRequestErrorResponse(ex.Message);
                return BadRequest(error);
            }
            catch (EntityNotFoundException ex)
            {
                var error = new NotFoundErrorResponse(ex.Message);
                return NotFound(error);
            }
            catch (InvalidOperationException ex)
            {
                var error = new ErrorResponse(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }
            catch (Exception ex)
            {
                var error = new ErrorResponse(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }
        }
    }

}
