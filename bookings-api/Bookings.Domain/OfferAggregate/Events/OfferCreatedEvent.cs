using Bookings.Domain.Common.Models.Events;

namespace Bookings.Domain.OfferAggregate.Events
{
    public record OfferCreatedEvent(Offer Offer) : IBeforeSaveDomainEvent;
}
