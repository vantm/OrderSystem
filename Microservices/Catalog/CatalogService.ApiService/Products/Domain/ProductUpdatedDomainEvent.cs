﻿namespace CatalogService.ApiService.Products.Domain;

public record ProductUpdatedDomainEvent : INotification
{
    public required Guid Id { get; init; }
    public required string PreviousName { get; init; } = string.Empty;
    public required string Name { get; init; } = string.Empty;
    public required DateTime UpdatedAt { get; init; }
}