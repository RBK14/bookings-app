using Bookings.Domain.Common.Models;

namespace Bookings.Domain.OfferAggregate.Events
{
    public record OfferCreatedEvent(Offer Offer) : IDomainEvent;
}
