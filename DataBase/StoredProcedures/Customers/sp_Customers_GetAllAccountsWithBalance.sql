CREATE PROCEDURE [dbo].[sp_Customers_GetAllAccountsWithBalance]
    @CustomerId UNIQUEIDENTIFIER
AS
BEGIN
    SELECT 
        [Id] AS [BillingNumberId],
        [AccountNumber],
        [Balance],
        [Currency],
        [Status],
        [AccountType],
        [CreatedAt],
        [UpdatedAt]
    FROM [dbo].[BillingNumbers]
    WHERE [CustomerId] = @CustomerId
    ORDER BY [CreatedAt] DESC;
END
