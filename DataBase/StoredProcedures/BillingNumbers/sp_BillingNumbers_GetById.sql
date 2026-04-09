CREATE PROCEDURE [dbo].[sp_BillingNumbers_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SELECT [Id], [AccountNumber], [Balance], [Currency], [AccountType], [Status], [CreatedAt], [UpdatedAt], [CustomerId]
    FROM [dbo].[BillingNumbers]
    WHERE [Id] = @Id
END
