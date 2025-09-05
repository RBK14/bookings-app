namespace Bookings.Application.Common.Interfaces.Persistence
{
    public interface IBaseRepository
    {
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }

}
