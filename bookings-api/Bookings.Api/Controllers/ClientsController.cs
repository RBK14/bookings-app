using Bookings.Application.Clients.Queries.GetClient;
using Bookings.Application.Clients.Queries.GetClients;
using Bookings.Contracts.Clients;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bookings.Api.Controllers
{
    [Route("api/clients")]
    public class ClientsController(ISender mediator, IMapper mapper) : ApiController
    {
        private readonly ISender _mediator = mediator;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        public async Task<IActionResult> GetClients()
        {
            var query = new GetClientsQuery();

            var result = await _mediator.Send(query);

            var response = result
                .Select(c => _mapper.Map<ClientResponse>(c))
                .ToList();

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClient(string id)
        {
            var query = new GetClientQuery(id);

            var result = await _mediator.Send(query);

            return result.Match(
                client => Ok(_mapper.Map<ClientResponse>(client)),
                errors => Problem(errors)
                );
        }
    }
}
