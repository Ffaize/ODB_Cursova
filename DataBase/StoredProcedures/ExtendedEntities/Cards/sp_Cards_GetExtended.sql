CREATE PROCEDURE [dbo].[sp_Cards_GetExtended]
AS
BEGIN
    SELECT 
        c.[Id], c.[CardNumber], c.[Status], c.[CardHolderName], c.[LaunchDate], c.[ExpirationDate], 
        c.[Cvv], c.[BillingNumberId], c.[CustomerId],
        bn.[Id] AS BillingNumberId, bn.[AccountNumber], bn.[Balance], bn.[Currency] AS BNCurrency, 
        bn.[AccountType], bn.[Status] AS BNStatus, bn.[CreatedAt] AS BNCreatedAt, 
        bn.[UpdatedAt], bn.[CustomerId] AS BNCustomerId,
        cu.[Id] AS CustomerId, cu.[Name] AS CustomerName, cu.[Surname], cu.[Email], 
        cu.[PasswordHash], cu.[CreatedAt] AS CustomerCreatedAt
    FROM [dbo].[Cards] c
    INNER JOIN [dbo].[BillingNumbers] bn ON c.[BillingNumberId] = bn.[Id]
    INNER JOIN [dbo].[Customers] cu ON c.[CustomerId] = cu.[Id]
    ORDER BY c.[LaunchDate] DESC
END
