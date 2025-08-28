namespace Bookings.Application.Common.Interfaces.Persistence
{
    public interface IFilterable<T>
    {
        IQueryable<T> Apply(IQueryable<T> query);
    }

}
