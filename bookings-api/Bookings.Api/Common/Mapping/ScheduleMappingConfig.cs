using Bookings.Application.Schedules.Commands.SetDefaultSchedule;
using Bookings.Application.Schedules.Commands.SetScheduleOverride;
using Bookings.Application.Schedules.Common;
using Bookings.Contracts.Schedule;
using Mapster;

namespace Bookings.Api.Common.Mapping
{
    public class ScheduleMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<(SetDefaultScheduleRequest Request, string EmployeeId), SetDefaultScheduleCommand>()
                .Map(dest => dest.EmployeeId, src => src.EmployeeId)
                .Map(dest => dest, src => src.Request);

            config.NewConfig<WorkDayScheduleDto, WorkDayScheduleCommandDto>();

            config.NewConfig<(SetScheduleOverrideRequest Request, string EmployeeId), SetScheduleOverrideCommand>()
                .Map(dest => dest.EmployeeId, src => src.EmployeeId)
                .Map(dest => dest, src => src.Request);

            config.NewConfig<WorkHoursResultDto, WorkHoursResponse>();
        }
    }
}
