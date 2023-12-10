CREATE TABLE [Identity].[User]
(
    [Id] UNIQUEIDENTIFIER PRIMARY KEY,
    [UserName] VARCHAR(100) NOT NULL,
    [PasswordHash] VARBINARY(4096) NULL,
    [PasswordSalt] VARBINARY(4096) NULL,
    [FullName] NVARCHAR(200) NULL,
    [EmailAddress] VARCHAR(200) NULL,
    [IsActive] BIT DEFAULT 0,
    [CreatedAt] DATETIME2(2) NULL,
    [UpdatedAt] DATETIME2(2) NULL,
    [DeletedAt] DATETIME2(2) NULL
)