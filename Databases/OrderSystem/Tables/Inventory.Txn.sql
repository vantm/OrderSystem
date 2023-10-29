CREATE TABLE [Inventory].[Txn]
(
    [Id]               UNIQUEIDENTIFIER PRIMARY KEY,
    [BatchId]          UNIQUEIDENTIFIER NOT NULL,
    [ProductId]        UNIQUEIDENTIFIER NOT NULL,
    [AdjustedQuantity] INT              NOT NULL,
    [CreatedAt]        DATETIME2        NOT NULL
)

GO

CREATE NONCLUSTERED INDEX [IX_Inventory_Txn_BatchId] ON [Inventory].[Txn] 
(
    [BatchId]
)
INCLUDE
(
    [ProductId]
)
