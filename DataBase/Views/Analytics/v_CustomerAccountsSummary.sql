CREATE VIEW [dbo].[v_CustomerAccountsSummary]
AS
SELECT 
    c.[Id],
    c.[Name],
    c.[Surname],
    c.[Email],
    COUNT(DISTINCT bn.[Id]) AS [AccountCount],
    ISNULL(SUM(bn.[Balance]), 0) AS [TotalBalance],
    COUNT(DISTINCT ca.[CardId]) AS [CardCount],
    COUNT(DISTINCT cr.[Id]) AS [ActiveCredits],
    c.[CreatedAt]
FROM [dbo].[Customers] c
LEFT JOIN [dbo].[BillingNumbers] bn ON c.[Id] = bn.[CustomerId]
LEFT JOIN (SELECT DISTINCT [Id] AS [CardId], [CustomerId] FROM [dbo].[Cards]) ca ON c.[Id] = ca.[CustomerId]
LEFT JOIN [dbo].[Credits] cr ON bn.[Id] = cr.[BillingNumberId] AND cr.[IsClosed] = 0
GROUP BY 
    c.[Id], c.[Name], c.[Surname], c.[Email], c.[CreatedAt]
ORDER BY 
    c.[Name], c.[Surname];
