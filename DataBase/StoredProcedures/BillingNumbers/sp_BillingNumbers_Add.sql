CREATE PROCEDURE [dbo].[sp_BillingNumbers_Add]
    @Id UNIQUEIDENTIFIER,
    @AccountNumber NVARCHAR(50),
    @Balance DECIMAL(18, 2),
    @Currency NVARCHAR(10),
    @AccountType INT,
    @Status INT,
    @CreatedAt DATETIME2 = NULL,
    @UpdatedAt DATETIME2 = NULL,
    @CustomerId UNIQUEIDENTIFIER
AS
BEGIN
    INSERT INTO [dbo].[BillingNumbers] ([Id], [AccountNumber], [Balance], [Currency], [AccountType], [Status], [CreatedAt], [UpdatedAt], [CustomerId])
    VALUES (@Id, @AccountNumber, @Balance, @Currency, @AccountType, @Status, ISNULL(@CreatedAt, GETUTCDATE()), ISNULL(@UpdatedAt, GETUTCDATE()), @CustomerId)
END
