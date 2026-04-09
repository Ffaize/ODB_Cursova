CREATE VIEW [dbo].[v_CustomerLoanRiskAssessment]
AS
SELECT TOP (100) PERCENT
    c.[Id],
    c.[Name] + ' ' + c.[Surname] AS [CustomerName],
    c.[Email],
    COUNT(DISTINCT cr.[Id]) AS [LoanCount],
    ISNULL(SUM(cr.[RemainingToPay]), 0) AS [TotalDebt],
    ISNULL(SUM(cr.[FullAmount]), 0) AS [TotalLoanAmount],
    CASE 
        WHEN ISNULL(SUM(cr.[FullAmount]), 0) = 0 THEN 0
        ELSE CAST((ISNULL(SUM(cr.[RemainingToPay]), 0) * 100.0 / ISNULL(SUM(cr.[FullAmount]), 0)) AS DECIMAL(5,2))
    END AS [DebtToLoanRatio],
    COUNT(DISTINCT bo.[Id]) AS [RecentTransactionCount],
    MAX(bo.[CreatedAt]) AS [LastTransactionDate],
    CASE 
        WHEN COUNT(DISTINCT cr.[Id]) > 3 AND ISNULL(SUM(cr.[RemainingToPay]), 0) > ISNULL(SUM(cr.[FullAmount]), 0) * 0.8 THEN 'High Risk'
        WHEN COUNT(DISTINCT cr.[Id]) > 1 AND ISNULL(SUM(cr.[RemainingToPay]), 0) > ISNULL(SUM(cr.[FullAmount]), 0) * 0.5 THEN 'Medium Risk'
        ELSE 'Low Risk'
    END AS [RiskLevel]
FROM [dbo].[Customers] c
LEFT JOIN [dbo].[BillingNumbers] bn ON c.[Id] = bn.[CustomerId]
LEFT JOIN [dbo].[Credits] cr ON bn.[Id] = cr.[BillingNumberId]
LEFT JOIN [dbo].[BillingOperations] bo ON (bo.[CustomerId] = c.[Id] OR bo.[BillingNumberIdFrom] = bn.[Id]) AND bo.[CreatedAt] > DATEADD(MONTH, -3, GETUTCDATE())
GROUP BY 
    c.[Id], c.[Name], c.[Surname], c.[Email]
ORDER BY 
    [RiskLevel] DESC, [DebtToLoanRatio] DESC;
