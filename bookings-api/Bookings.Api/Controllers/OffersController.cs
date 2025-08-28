using Bookings.Application.Offers.Commands.CreateOffer;
using Bookings.Application.Offers.Queries.GetOfferById;
using Bookings.Application.Offers.Queries.GetOffers;
using Bookings.Application.Offers.Queries.SearchOffers;
using Bookings.Contracts.Offers;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Bookings.Api.Controllers
{
    [Route("offers")]
    [Authorize]
    public class OffersController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public OffersController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetOffers()
        {
            var query = new GetOffersQuery();

            var result = await _mediator.Send(query);

            var response = result
                .Select(o => _mapper.Map<OfferResponse>(o))
                .ToList();

            return Ok(response);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchOffers(
            [FromQuery] string? name,
            [FromQuery] string? employeeId,
            [FromQuery] decimal? minPrice,
            [FromQuery] decimal? maxPrice,
            [FromQuery] int? currency,
            [FromQuery] string? minDuration,
            [FromQuery] string? maxDuration,
            [FromQuery] string? sortBy)
        {
            var query = new SearchOffersQuery(
                Name: name,
                EmployeeId: employeeId,
                MinPrice: minPrice,
                MaxPrice: maxPrice,
                Currency: currency,
                MinDuration: minDuration,
                MaxDuration: maxDuration,
                SortBy: sortBy
            );

            var result = await _mediator.Send(query);

            var response = result
                .Select(o => _mapper.Map<OfferResponse>(o))
                .ToList();

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOfferById(string id)
        {
            var query = new GetOfferByIdQuery(id);

            var result = await _mediator.Send(query);

            return result.Match(
                offer => Ok(_mapper.Map<OfferResponse>(offer)),
                errors => Problem(errors)
            );
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateOffer(CreateOfferRequest request)
        {
            // TODO: Przerobić na EmployeeId
            var employeeId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // TODO: Walidacja Id
            if (string.IsNullOrEmpty(employeeId))
                return Unauthorized();

            var command = _mapper.Map<CreateOfferCommand>((request, employeeId));

            var result = await _mediator.Send(command);

            return result.Match(
                offer => Ok(_mapper.Map<OfferResponse>(offer)),
                errors => Problem(errors)
            );
        }
    }
}
