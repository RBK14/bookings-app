using Bookings.Application.Offers.Commands.CreateOffer;
using Bookings.Application.Offers.Queries.GetOffers;
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
                errors => Problem(errors));
        }

        [HttpGet]
        public async Task<IActionResult> GetOffers()
        {
            var query = new GetOffersQuery();

            var offers = await _mediator.Send(query);

            var offerResponses = new List<OfferResponse>();

            foreach (var offer in offers)
            {
                var offerResponse = _mapper.Map<OfferResponse>(offer);
                offerResponses.Add(offerResponse);
            }

            return Ok(offerResponses);
        }
    }
}
