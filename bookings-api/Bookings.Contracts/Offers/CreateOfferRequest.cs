namespace Bookings.Contracts.Offers
{
    public record CreateOfferRequest(
        string Name,
        string Description,
        decimal Amount,
        int Currency,
        string Duration);
}
