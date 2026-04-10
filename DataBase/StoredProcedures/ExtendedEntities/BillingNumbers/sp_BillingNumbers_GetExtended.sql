CREATE PROCEDURE [dbo].[sp_BillingNumbers_GetExtended]
AS
BEGIN
    SELECT 
        bn.[Id], bn.[AccountNumber], bn.[Balance], bn.[Currency], bn.[AccountType], bn.[Status], 
        bn.[CreatedAt], bn.[UpdatedAt], bn.[CustomerId],
        c.[Id] AS CustomerId, c.[Name] AS CustomerName, c.[Surname], c.[Email], 
        c.[PasswordHash], c.[CreatedAt] AS CustomerCreatedAt
    FROM [dbo].[BillingNumbers] bn
    INNER JOIN [dbo].[Customers] c ON bn.[CustomerId] = c.[Id]
    ORDER BY bn.[CreatedAt] DESC
END
