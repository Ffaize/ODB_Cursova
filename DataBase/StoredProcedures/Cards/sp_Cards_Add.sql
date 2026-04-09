CREATE PROCEDURE [dbo].[sp_Cards_Add]
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
    INSERT INTO [dbo].[Cards] ([Id], [CardNumber], [Status], [CardHolderName], [LaunchDate], [ExpirationDate], [cvv], [BillingNumberId], [CustomerId])
    VALUES (@Id, @CardNumber, @Status, @CardHolderName, GETUTCDATE(), @ExpirationDate, @cvv, @BillingNumberId, @CustomerId)
END
