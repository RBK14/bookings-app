using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Domain.AppointmentAggregate;

namespace Bookings.Application.Appointments.Options
{
    public class AppointmentSort(string? sortBy) : ISortable<Appointment>
    {
        private readonly string? _sortBy = sortBy;

        public IQueryable<Appointment> Apply(IQueryable<Appointment> query)
        {
            return _sortBy switch
            {
                "NameAsc" => query.OrderBy(a => a.OfferName),
                "NameDesc" => query.OrderByDescending(a => a.OfferName),
                "PriceAsc" => query.OrderBy(a => a.OfferPrice.Amount),
                "PriceDesc" => query.OrderByDescending(a => a.OfferPrice.Amount),
                "DurationAsc" => query.OrderBy(a => a.OfferDuration.Value),
                "DurationDesc" => query.OrderByDescending(a => a.OfferDuration.Value),
                "StartTimeAsc" => query.OrderBy(a => a.Time.Start),
                "StartTimeDesc" => query.OrderByDescending(a => a.Time.Start),
                _ => query.OrderBy(a => a.OfferName)
            };
        }
    }

}
