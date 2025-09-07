using Bookings.Application.Appointments.Options;
using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Application.Schedules.Common;
using Bookings.Domain.AppointmentAggregate;
using Bookings.Domain.ClientAggregate.ValueObjects;
using Bookings.Domain.Common.Errors;
using Bookings.Domain.EmployeeAggregate.ValueObjects;
using Bookings.Domain.ScheduleAggregate;
using ErrorOr;
using MediatR;
using System.Threading.Tasks;

namespace Bookings.Application.Schedules.Queries.GetSchedule
{
    public class GetScheduleQueryHandler(
        IScheduleRepository scheduleRepository,
        IAppointmentRepository appointmentRepository,
        IClientRepository clientRepository,
        IUserRepository userRepository) : IRequestHandler<GetScheduleQuery, ErrorOr<IEnumerable<DayScheduleResultDto>>>
    {
        private readonly IScheduleRepository _scheduleRepository = scheduleRepository;
        private readonly IAppointmentRepository _appointmentRepository = appointmentRepository;
        private readonly IClientRepository _clientRepository = clientRepository;
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<ErrorOr<IEnumerable<DayScheduleResultDto>>> Handle(GetScheduleQuery query, CancellationToken cancellationToken)
        {
            var employeeId = EmployeeId.Create(query.EmployeeId);

            if (await _scheduleRepository.GetByEmployeeIdAsync(employeeId) is not Schedule schedule)
                return Errors.Schedule.NotFound;

            var today = DateOnly.FromDateTime(DateTime.UtcNow);

            DateOnly fromDate;
            DateOnly toDate;

            if (query.From.HasValue && query.To.HasValue)
            {
                fromDate = query.From.Value;
                toDate = query.To.Value;
            }
            else if (query.Days.HasValue)
            {
                fromDate = today;
                toDate = today.AddDays(query.Days.Value);
            }
            else
            {
                fromDate = today;
                toDate = today.AddDays(7);
            }

            var fromDateTime = fromDate.ToDateTime(TimeOnly.MinValue);
            var toDateTime = toDate.ToDateTime(TimeOnly.MinValue);

            var filters = new List<IFilterable<Appointment>>
            {
                new EmployeeIdFilter(employeeId),
                new StartTimeRangeFilter(fromDateTime, toDateTime)
            };

            var appointments = await _appointmentRepository.SearchAsync(filters);

            var clientDict = await GetClientNameDict(appointments);

            var results = new List<DayScheduleResultDto>();

            foreach (var day in EachDay(fromDate, toDate))
            {
                // TODO: Dorobić sprawdzenie nadpisań

                var workDay = schedule.DefaultSchedules.FirstOrDefault(s => s.DayOfWeek == day.DayOfWeek);
                if (workDay is null)
                    continue;

                var dayAppointments = appointments
                    .Where(a => DateOnly.FromDateTime(a.Time.Start) == day)
                    .Select(a => new AppointmentSlotResultDto(
                        a.Id.Value.ToString(),
                        TimeOnly.FromDateTime(a.Time.Start),
                        TimeOnly.FromDateTime(a.Time.End),
                        a.OfferName,
                        clientDict.TryGetValue(a.ClientId, out var clientName) ? clientName : "Unknown"
                    ));

                results.Add(new DayScheduleResultDto(
                    day,
                    workDay.Start,
                    workDay.End,
                    dayAppointments.ToList()
                ));
            }

            return results;
        }

        private static IEnumerable<DateOnly> EachDay(DateOnly from, DateOnly to)
        {
            for (var date = from; date <= to; date = date.AddDays(1))
                yield return date;
        }

        private async Task<IDictionary<ClientId, string>> GetClientNameDict(IEnumerable<Appointment> appointments)
        {
            var clientIds = appointments.Select(a => a.ClientId).Distinct().ToList();
            var clients = await _clientRepository.GetByIdsAsync(clientIds);

            var usersIds = clients.Select(a => a.UserId).Distinct().ToList();
            var users = await _userRepository.GetByIdsAsync(usersIds);

            var userDict = users.ToDictionary(u => u.Id, u => (u.FirstName + " " + u.LastName));

            var clientNameDict = clients.ToDictionary(
                c => c.Id,
                c => userDict.TryGetValue(c.UserId, out var name) ? name : "Unknown"
            );

            return clientNameDict;
        }
    }
}
