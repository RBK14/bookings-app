using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Domain.VerificationTokenAggregate;
using Bookings.Domain.VerificationTokenAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Bookings.Infrastructure.Persistence.Repositories
{
    public class VerificationTokenRepository(BookingsDbContext dbContext) : BaseRepository(dbContext), IVerificationTokenRepository
    {
        public void Add(VerificationToken verificationToken)
        {
            _dbContext.VerificationTokens.Add(verificationToken);
        }

        public async Task<VerificationToken?> GetByIdAsync(VerificationTokenId verificationTokenId)
        {
            return await _dbContext.VerificationTokens
                .SingleOrDefaultAsync(vt => vt.Id == verificationTokenId);
        }

        public void Update(VerificationToken verificationToken)
        {
            _dbContext.VerificationTokens.Update(verificationToken);
        }

        public void Delete(VerificationToken verificationToken)
        {
            _dbContext.VerificationTokens.Remove(verificationToken);
        }
    }
}
