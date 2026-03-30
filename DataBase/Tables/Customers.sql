CREATE TABLE [dbo].[Customers] (
    [Id]              UNIQUEIDENTIFIER PRIMARY KEY NOT NULL DEFAULT (NEWID()),
    [Name]            NVARCHAR(MAX) NOT NULL,
    [Surname]         NVARCHAR(MAX) NOT NULL,
    [Email]           NVARCHAR(MAX) NOT NULL,
    [PasswordHash]    NVARCHAR(MAX) NOT NULL,
    [CreatedAt]       DATETIME2 NOT NULL DEFAULT (GETUTCDATE())
);
