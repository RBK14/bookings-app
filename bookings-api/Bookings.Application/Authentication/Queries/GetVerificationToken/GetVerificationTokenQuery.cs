using Bookings.Domain.VerificationTokenAggregate;
using ErrorOr;
using MediatR;

namespace Bookings.Application.Authentication.Queries.GetVerificationToken
{
    public record GetVerificationTokenQuery(string Id) : IRequest<ErrorOr<VerificationToken>>;
}
