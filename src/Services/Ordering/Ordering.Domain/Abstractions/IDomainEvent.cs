using MediatR;

namespace Ordering.Domain.Abstractions;

public interface IDomainEvent : INotification
{
    Guid EventId => Guid.NewGuid();
    DateTime OcuredOn => DateTime.Now;
    string? EventType => GetType().AssemblyQualifiedName;
}