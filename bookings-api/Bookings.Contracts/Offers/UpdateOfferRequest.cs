namespace Bookings.Contracts.Offers
{
    public record UpdateOfferRequest (
        string Name,
        string Description,
        decimal Amount,
        int Currency,
        TimeSpan Duration);
}
