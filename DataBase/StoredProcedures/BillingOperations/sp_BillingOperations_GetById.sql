CREATE PROCEDURE [dbo].[sp_BillingOperations_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SELECT [Id], [Amount], [Currency], [CreatedAt], [Description], [PaymentPurpose], [CustomerId], [BillingNumberIdFrom], [BillingNumberIdTo], [CreditId]
    FROM [dbo].[BillingOperations]
    WHERE [Id] = @Id
END
