namespace Bookings.Contracts.Offers
{
    public record OfferResponse(
        string Id,
        string Name,
        string Description,
        string EmployeeId,
        decimal Amount,
        int Currency,
        TimeSpan Duration,
        List<string> Appointments,
        DateTime CreatedAt,
        DateTime UpdatedAt);
}
