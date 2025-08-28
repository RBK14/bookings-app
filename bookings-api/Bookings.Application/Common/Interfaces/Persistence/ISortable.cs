namespace Bookings.Application.Common.Interfaces.Persistence
{
    public interface ISortable<T>
    {
        IQueryable<T> Apply(IQueryable<T> query);
    }

}
