CREATE PROCEDURE [dbo].[sp_Cards_Add]
    @Id UNIQUEIDENTIFIER,
    @CardNumber NVARCHAR(16),
    @Status INT,
    @CardHolderName NVARCHAR(255),
    @LaunchDate DATETIME2 = NULL,
    @ExpirationDate DATETIME2,
    @Cvv INT,
    @BillingNumberId UNIQUEIDENTIFIER,
    @CustomerId UNIQUEIDENTIFIER
AS
BEGIN
    INSERT INTO [dbo].[Cards] ([Id], [CardNumber], [Status], [CardHolderName], [LaunchDate], [ExpirationDate], [cvv], [BillingNumberId], [CustomerId])
    VALUES (@Id, @CardNumber, @Status, @CardHolderName, ISNULL(@LaunchDate, GETUTCDATE()), @ExpirationDate, @Cvv, @BillingNumberId, @CustomerId)
END
