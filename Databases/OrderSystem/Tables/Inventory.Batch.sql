CREATE TABLE [Inventory].[Batch]
(
    [Id]        UNIQUEIDENTIFIER PRIMARY KEY,
    [ProductId] UNIQUEIDENTIFIER NOT NULL,
    [Price]     MONEY            NOT NULL,
    [Quantity]  INT              NOT NULL,
    [IsActive]  BIT              NOT NULL DEFAULT 0,
    [IsDeleted] BIT              NOT NULL DEFAULT 0,
    [CreatedAt] DATETIME2        NOT NULL,
    [UpdatedAt] DATETIME2        NOT NULL
)

GO

CREATE NONCLUSTERED INDEX [IX_Inventory_Batch_ProductId] ON [Inventory].[Batch] 
(
    [ProductId]
)
INCLUDE
(
    [Price],
    [Quantity],
    [IsActive]
)