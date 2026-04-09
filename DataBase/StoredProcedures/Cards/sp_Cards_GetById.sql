CREATE PROCEDURE [dbo].[sp_Cards_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SELECT [Id], [CardNumber], [Status], [CardHolderName], [LaunchDate], [ExpirationDate], [cvv], [BillingNumberId], [CustomerId]
    FROM [dbo].[Cards]
    WHERE [Id] = @Id
END
