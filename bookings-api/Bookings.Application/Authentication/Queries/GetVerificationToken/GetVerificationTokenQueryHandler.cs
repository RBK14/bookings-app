using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Domain.Common.Errors;
using Bookings.Domain.VerificationTokenAggregate;
using Bookings.Domain.VerificationTokenAggregate.ValueObjects;
using ErrorOr;
using MediatR;

namespace Bookings.Application.Authentication.Queries.GetVerificationToken
{
    public class GetVerificationTokenQueryHandler(IVerificationTokenRepository verificationTokenRepository) : IRequestHandler<GetVerificationTokenQuery, ErrorOr<VerificationToken>>
    {
        private readonly IVerificationTokenRepository _verificationTokenRepository = verificationTokenRepository;

        public async Task<ErrorOr<VerificationToken>> Handle(GetVerificationTokenQuery query, CancellationToken cancellationToken)
        {
            var tokenId = VerificationTokenId.Create(query.Id);

            if (await _verificationTokenRepository.GetByIdAsync(tokenId) is not VerificationToken token)
                return Errors.Authentication.InvalidVerificationToken;

            return token;
        }
    }
}
