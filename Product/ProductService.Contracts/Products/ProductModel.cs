﻿namespace ProductService.Contracts.Products;

public record ProductModel(
    Guid Id,
    string Name,
    DateTime CreatedAt,
    DateTime UpdatedAt);