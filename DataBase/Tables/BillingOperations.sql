CREATE TABLE [dbo].[BillingOperations]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
	[Amount] DECIMAL(18, 2) NOT NULL,
	[Currency] NVARCHAR(3) NOT NULL,
	[CreatedAt] DATETIME2 NOT NULL,
	[Description] NVARCHAR(255) NULL,
	[PaymentPurpose] INT NOT NULL,
	[CustomerId] UNIQUEIDENTIFIER NULL DEFAULT NULL,
	[BillingNumberIdFrom] UNIQUEIDENTIFIER NULL DEFAULT NULL,
	[BillingNumberIdTo] UNIQUEIDENTIFIER NULL DEFAULT NULL,
	[CreditId] UNIQUEIDENTIFIER NULL DEFAULT NULL,
	CONSTRAINT [FK_BillingOperations_Customers] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customers]([Id]),
	CONSTRAINT [FK_BillingOperations_BillingNumbers_From] FOREIGN KEY ([BillingNumberIdFrom]) REFERENCES [dbo].[BillingNumbers]([Id]),
	CONSTRAINT [FK_BillingOperations_BillingNumbers_To] FOREIGN KEY ([BillingNumberIdTo]) REFERENCES [dbo].[BillingNumbers]([Id]),
	CONSTRAINT [FK_BillingOperations_Credits] FOREIGN KEY ([CreditId]) REFERENCES [dbo].[Credits]([Id])
)
