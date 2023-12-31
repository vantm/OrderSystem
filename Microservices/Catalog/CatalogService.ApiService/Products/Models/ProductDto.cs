﻿namespace CatalogService.ApiService.Products.Models;

public record ProductDto
{
    public required Guid Id { get; init; }
    public required string Name { get; init; } = string.Empty;
    public required DateTime CreatedAt { get; init; }
    public required DateTime UpdatedAt { get; init; }
}