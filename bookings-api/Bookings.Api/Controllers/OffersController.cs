using Bookings.Application.Offers.Commands.CreateOffer;
using Bookings.Application.Offers.Commands.DeleteOffer;
using Bookings.Application.Offers.Commands.UpdateOffer;
using Bookings.Application.Offers.Queries.GetEmployeeOffers;
using Bookings.Application.Offers.Queries.GetOfferById;
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
    public class OffersController(ISender mediator, IMapper mapper) : ApiController
    {
        private readonly ISender _mediator = mediator;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        public async Task<IActionResult> GetOffers(
            [FromQuery] string? name,
            [FromQuery] string? employeeId,
            [FromQuery] decimal? minPrice,
            [FromQuery] decimal? maxPrice,
            [FromQuery] int? currency,
            [FromQuery] string? minDuration,
            [FromQuery] string? maxDuration,
            [FromQuery] string? sortBy)
        {
            var query = new GetOffersQuery(
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

        [HttpPost]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> CreateOffer(CreateOfferRequest request)
        {
            var employeeId = User.FindFirst("RoleId")?.Value;

            if (string.IsNullOrEmpty(employeeId))
                return Problem(statusCode: StatusCodes.Status401Unauthorized, title: "Identyfikator pracownika jest nieprawidłowy.");

            var command = _mapper.Map<CreateOfferCommand>((request, employeeId));

            var result = await _mediator.Send(command);

            return result.Match(
                offer => Ok(_mapper.Map<OfferResponse>(offer)),
                errors => Problem(errors)
            );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOffer(string id)
        {
            var query = new GetOfferByIdQuery(id);

            var result = await _mediator.Send(query);

            return result.Match(
                offer => Ok(_mapper.Map<OfferResponse>(offer)),
                errors => Problem(errors)
            );
        }

        [HttpPost("{id}")]
        [Authorize(Roles = "Admin, Employee")]
        public async Task<IActionResult> UpdateOffer(UpdateOfferRequest request, string id)
        {
            var employeeId = User.FindFirst("RoleId")?.Value;

            if (string.IsNullOrEmpty(employeeId))   // If empty => UserRole: Admin
                employeeId = Guid.Empty.ToString(); // Setting employeeId value for admin

            var command = _mapper.Map<UpdateOfferCommand>((request, id, employeeId));

            var result = await _mediator.Send(command);

            return result.Match(
                offer => Ok(_mapper.Map<OfferResponse>(offer)),
                errors => Problem(errors)
            );
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Employee")]
        public async Task<IActionResult> DeleteOffer(string id)
        {
            var employeeId = User.FindFirst("RoleId")?.Value;

            if (string.IsNullOrEmpty(employeeId))   // If empty => UserRole: Admin
                employeeId = Guid.Empty.ToString(); // Setting employeeId value for admin

            var command = new DeleteOfferCommand(id, employeeId);

            var result = await _mediator.Send(command);

            return result.Match(
                r => Ok(),
                errors => Problem(errors));
        }

        // TODO: Endpoint do usunięcia
        [HttpGet("myoffers")]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> GetEmployeeOffers(
            [FromQuery] string? name,
            [FromQuery] decimal? minPrice,
            [FromQuery] decimal? maxPrice,
            [FromQuery] int? currency,
            [FromQuery] string? minDuration,
            [FromQuery] string? maxDuration,
            [FromQuery] string? sortBy)
        {
            var employeeId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(employeeId))
                return Problem(statusCode: StatusCodes.Status401Unauthorized, title: "Identyfikator pracownika jest nieprawidłowy.");

            var query = new GetEmployeeOffersQuery(
                EmployeeId: employeeId,
                Name: name,
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
    }
}
