namespace Bookings.Contracts.Employees
{
    public record EmployeeResponse(
        string Id,
        string UserId,
        List<string> OfferIds,
        List<string> AppointmentIds);
}
