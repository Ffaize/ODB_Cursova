CREATE TABLE [dbo].[BillingNumbers] (
    [Id]              UNIQUEIDENTIFIER PRIMARY KEY NOT NULL DEFAULT (NEWID()),
    [AccountNumber]   NVARCHAR(50) NOT NULL UNIQUE,
    [Balance]         DECIMAL(18, 2) NOT NULL DEFAULT (0),
    [Currency]        NVARCHAR(10) NOT NULL DEFAULT ('UAH'),
    [AccountType]     INT NOT NULL DEFAULT (1),
    [Status]          INT NOT NULL DEFAULT (1),
    [CreatedAt]       DATETIME2 NOT NULL DEFAULT (GETUTCDATE()),
    [UpdatedAt]       DATETIME2 NOT NULL DEFAULT (GETUTCDATE()),
    [UserId]          UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [FK_BillingNumbers_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users]([Id])
);
