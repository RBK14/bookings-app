using Bookings.Domain.VerificationTokenAggregate;
using Bookings.Domain.VerificationTokenAggregate.ValueObjects;

namespace Bookings.Application.Common.Interfaces.Persistence
{
    public interface IVerificationTokenRepository : IBaseRepository
    {
        void Add(VerificationToken verificationToken);
        Task<VerificationToken?> GetById(VerificationTokenId verificationTokenId);
        void Update(VerificationToken verificationToken);
        void Delete(VerificationToken verificationToken);
    }
}
