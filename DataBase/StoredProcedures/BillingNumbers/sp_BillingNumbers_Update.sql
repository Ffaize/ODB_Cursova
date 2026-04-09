CREATE PROCEDURE [dbo].[sp_BillingNumbers_Update]
    @Id UNIQUEIDENTIFIER,
    @AccountNumber NVARCHAR(50),
    @Balance DECIMAL(18, 2),
    @Currency NVARCHAR(10),
    @AccountType INT,
    @Status INT,
    @CustomerId UNIQUEIDENTIFIER
AS
BEGIN
    UPDATE [dbo].[BillingNumbers]
    SET [AccountNumber] = @AccountNumber,
        [Balance] = @Balance,
        [Currency] = @Currency,
        [AccountType] = @AccountType,
        [Status] = @Status,
        [UpdatedAt] = GETUTCDATE(),
        [CustomerId] = @CustomerId
    WHERE [Id] = @Id
END
