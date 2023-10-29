CREATE TABLE [Catalog].[ProductImage]
(
    [Id]        UNIQUEIDENTIFIER PRIMARY KEY,
    [ProductId] UNIQUEIDENTIFIER NOT NULL,
    [Data]      BINARY           NOT NULL,
    [CreatedAt] DATETIME2        NOT NULL
)

GO

CREATE NONCLUSTERED INDEX [IX_Catalog_ProductImage_ProductId] ON [Catalog].[ProductImage]
(
    [ProductId]
)
INCLUDE 
(
    [Data]
)
