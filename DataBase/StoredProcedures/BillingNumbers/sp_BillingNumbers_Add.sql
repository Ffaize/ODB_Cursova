CREATE PROCEDURE [dbo].[sp_BillingNumbers_Add]
    @Id UNIQUEIDENTIFIER,
    @AccountNumber NVARCHAR(50),
    @Balance DECIMAL(18, 2),
    @Currency NVARCHAR(10),
    @AccountType INT,
    @Status INT,
    @CustomerId UNIQUEIDENTIFIER
AS
BEGIN
    INSERT INTO [dbo].[BillingNumbers] ([Id], [AccountNumber], [Balance], [Currency], [AccountType], [Status], [CreatedAt], [UpdatedAt], [CustomerId])
    VALUES (@Id, @AccountNumber, @Balance, @Currency, @AccountType, @Status, GETUTCDATE(), GETUTCDATE(), @CustomerId)
END
