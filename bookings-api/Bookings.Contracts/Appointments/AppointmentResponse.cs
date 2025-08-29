namespace Bookings.Contracts.Appointments
{
    public record AppointmentResponse(
        string Id,
        string OfferId,
        string OfferName,
        decimal OfferAmount,
        int OfferCurrency,
        string OfferDuration,
        string EmployeeId,
        string ClientId,
        DateTime StartTime,
        DateTime EndTime,
        string Status,
        string CancelledBy,
        DateTime CreatedAt,
        DateTime UpdatedAt
    );
}
