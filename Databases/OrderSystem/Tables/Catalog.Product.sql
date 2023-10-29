CREATE TABLE [Catalog].[Product]
(
    [Id]        UNIQUEIDENTIFIER PRIMARY KEY,
    [Name]      NVARCHAR(200)    NOT NULL,
    [IsActive]  BIT              NOT NULL DEFAULT 0,
    [IsDeleted] BIT              NOT NULL DEFAULT 0,
    [CreatedAt] DATETIME2        NOT NULL,
    [UpdatedAt] DATETIME2        NOT NULL
)

GO