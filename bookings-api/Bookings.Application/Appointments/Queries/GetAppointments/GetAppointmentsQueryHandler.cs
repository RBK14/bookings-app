using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Application.Appointments.Options;
using Bookings.Domain.AppointmentAggregate;
using Bookings.Domain.AppointmentAggregate.Enums;
using Bookings.Domain.ClientAggregate.ValueObjects;
using Bookings.Domain.Common.Enums;
using Bookings.Domain.Common.ValueObjects;
using Bookings.Domain.EmployeeAggregate.ValueObjects;
using MediatR;

namespace Bookings.Application.Appointments.Queries.GetAppointments
{
    public class GetAppointmentsQueryHandler(
        IAppointmentRepository appointmentsRepository) : IRequestHandler<GetAppointmentsQuery, IEnumerable<Appointment>>
    {
        private readonly IAppointmentRepository _appointmentsRepository = appointmentsRepository;

        public async Task<IEnumerable<Appointment>> Handle(GetAppointmentsQuery query, CancellationToken cancellationToken)
        {
            var currency = CurrencyExtensions.IsCorrectCurrencyCode(query.Currency)
                ? CurrencyExtensions.FromCode(query.Currency)
                : Currency.PLN; // If null => PLN by default

            var minPrice = query.MinPrice is decimal
                ? Price.Create(query.MinPrice.Value, currency)
                : null;

            var maxPrice = query.MaxPrice is decimal
                ? Price.Create(query.MaxPrice.Value, currency)
                : null;

            var minDuration = !string.IsNullOrWhiteSpace(query.MinDuration)
                ? Duration.Create(query.MinDuration)
                : null;

            var maxDuration = !string.IsNullOrWhiteSpace(query.MaxDuration)
                ? Duration.Create(query.MaxDuration)
                : null;

            var employeeId = !string.IsNullOrWhiteSpace(query.EmployeeId)
               ? EmployeeId.Create(query.EmployeeId)
               : null;

            var clientId = !string.IsNullOrWhiteSpace(query.ClientId)
                ? ClientId.Create(query.ClientId)
                : null;

            var status = !string.IsNullOrWhiteSpace(query.Status) && Enum.TryParse<AppointmentStatus>(query.Status, true, out var parsedStatus)
                ? parsedStatus
                : (AppointmentStatus?)null;

            var filters = new List<IFilterable<Appointment>>
            {
                new NameFilter(query.Name),
                new PriceRangeFilter(minPrice, maxPrice),
                new DurationRangeFilter(minDuration, maxDuration),
                new EmployeeIdFilter(employeeId),
                new ClientIdFilter(clientId),
                new StartTimeRangeFilter(query.Starts, query.Ends),
                new StatusFilter(status)
            };

            var sort = new AppointmentSort(query.SortBy);

            return await _appointmentsRepository.SearchAsync(filters, sort);
        }
    }
}
        
