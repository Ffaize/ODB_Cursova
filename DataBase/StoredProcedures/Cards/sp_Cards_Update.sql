CREATE PROCEDURE [dbo].[sp_Cards_Update]
    @Id UNIQUEIDENTIFIER,
    @CardNumber NVARCHAR(16),
    @Status INT,
    @CardHolderName NVARCHAR(255),
    @ExpirationDate DATETIME2,
    @cvv INT,
    @BillingNumberId UNIQUEIDENTIFIER,
    @CustomerId UNIQUEIDENTIFIER
AS
BEGIN
    UPDATE [dbo].[Cards]
    SET [CardNumber] = @CardNumber,
        [Status] = @Status,
        [CardHolderName] = @CardHolderName,
        [ExpirationDate] = @ExpirationDate,
        [cvv] = @cvv,
        [BillingNumberId] = @BillingNumberId,
        [CustomerId] = @CustomerId
    WHERE [Id] = @Id
END
