CREATE TABLE [dbo].[Cards]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
	[CardNumber] NVARCHAR(16) NOT NULL,
	[Status] INT NOT NULL,
	[CardHolderName] NVARCHAR(255) NOT NULL,
	[LaunchDate] DATETIME2 NOT NULL DEFAULT (GETUTCDATE()),
	[ExpirationDate] DATETIME2 NOT NULL,
	[cvv] int NOT NULL,
	[BillingNumberId] UNIQUEIDENTIFIER NOT NULL,
	[CustomerId] UNIQUEIDENTIFIER NOT NULL,
	CONSTRAINT [FK_Cards_BillingNumbers] FOREIGN KEY ([BillingNumberId]) REFERENCES [dbo].[BillingNumbers]([Id]),
	CONSTRAINT [FK_Cards_Customers] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customers]([Id])
)
