﻿using MediatR;

namespace ProductService.ApiService.Products.DomainEvents;

public record ProductCreatedDomainEvent : INotification
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required DateTime CreatedAt { get; init; }
}