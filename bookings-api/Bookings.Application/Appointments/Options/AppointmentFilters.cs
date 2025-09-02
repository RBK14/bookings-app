using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Domain.AppointmentAggregate;
using Bookings.Domain.AppointmentAggregate.Enums;
using Bookings.Domain.ClientAggregate.ValueObjects;
using Bookings.Domain.Common.ValueObjects;
using Bookings.Domain.EmployeeAggregate.ValueObjects;

namespace Bookings.Application.Appointments.Options
{
    public class NameFilter(string? name) : IFilterable<Appointment>
    {
        private readonly string? _name = name;

        public IQueryable<Appointment> Apply(IQueryable<Appointment> query)
        {
            if (string.IsNullOrWhiteSpace(_name))
                return query;

            return query.Where(a => a.OfferName.Contains(_name));
        }
    }

    public class PriceRangeFilter : IFilterable<Appointment>
    {
        private readonly Price? _minPrice;
        private readonly Price? _maxPrice;

        public PriceRangeFilter(Price? minPrice, Price? maxPrice)
        {
            _minPrice = minPrice;
            _maxPrice = maxPrice;
        }

        public IQueryable<Appointment> Apply(IQueryable<Appointment> query)
        {
            if (_minPrice is not null)
                query = query.Where(a => a.OfferPrice.Amount >= _minPrice.Amount);

            if (_maxPrice is not null)
                query = query.Where(a => a.OfferPrice.Amount <= _maxPrice.Amount);

            return query;
        }
    }

    public class DurationRangeFilter(Duration? minDuration, Duration? maxDuration) : IFilterable<Appointment>
    {
        private readonly Duration? _minDuration = minDuration;
        private readonly Duration? _maxDuration = maxDuration;

        public IQueryable<Appointment> Apply(IQueryable<Appointment> query)
        {
            if (_minDuration is not null)
                query = query.Where(a => a.OfferDuration.Value >= _minDuration.Value);

            if (_maxDuration is not null)
                query = query.Where(a => a.OfferDuration.Value <= _maxDuration.Value);

            return query;
        }
    }

    public class EmployeeIdFilter(EmployeeId? employeeId) : IFilterable<Appointment>
    {
        private readonly EmployeeId? _employeeID = employeeId;

        public IQueryable<Appointment> Apply(IQueryable<Appointment> query)
        {
            if (_employeeID is not null && !string.IsNullOrWhiteSpace(_employeeID.Value.ToString()))
                query = query.Where(a => a.EmployeeId.Equals(_employeeID));

            return query;
        }
    }

    public class ClientIdFilter(ClientId? clientId) : IFilterable<Appointment>
    {
        private readonly ClientId? _clientId = clientId;

        public IQueryable<Appointment> Apply(IQueryable<Appointment> query)
        {
            if (_clientId is not null && !string.IsNullOrWhiteSpace(_clientId.Value.ToString()))
                query = query.Where(a => a.EmployeeId.Equals(_clientId));

            return query;
        }
    }

    public class StartTimeRangeFilter(DateTime? starts, DateTime? ends) : IFilterable<Appointment>
    {
        private readonly DateTime? _starts = starts;
        private readonly DateTime? _ends = ends;

        public IQueryable<Appointment> Apply(IQueryable<Appointment> query)
        {
            if (_starts.HasValue)
            {
                query = query.Where(a => a.Time.Start >= _starts.Value);
            }

            if (_ends.HasValue)
            {
                query = query.Where(a => a.Time.Start <= _ends.Value);
            }

            return query;
        }
    }

    public class StatusFilter(AppointmentStatus? status) : IFilterable<Appointment>
    {
        private readonly AppointmentStatus? _status = status;

        public IQueryable<Appointment> Apply(IQueryable<Appointment> query)
        {
            if (_status.HasValue)
                query = query.Where(a => a.Status == _status.Value);

            return query;
        }
    }
}
