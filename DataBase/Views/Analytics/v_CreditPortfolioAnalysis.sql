CREATE VIEW [dbo].[v_CreditPortfolioAnalysis]
AS
SELECT TOP (100) PERCENT
    c.[Id],
    cu.[Name] + ' ' + cu.[Surname] AS [CustomerName],
    c.[FullAmount],
    c.[RemainingToPay],
    c.[FullAmount] - c.[RemainingToPay] AS [AmountPaid],
    CAST(((c.[FullAmount] - c.[RemainingToPay]) * 100.0 / c.[FullAmount]) AS DECIMAL(5,2)) AS [PercentagePaid],
    c.[MonthlyPayment],
    c.[DurationInMonths],
    c.[Currency],
    CASE WHEN c.[IsClosed] = 1 THEN 'Closed' ELSE 'Active' END AS [Status],
    CASE 
        WHEN c.[IsClosed] = 1 THEN 0
        WHEN DATEDIFF(DAY, GETUTCDATE(), c.[NextPayment]) < 0 THEN DATEDIFF(DAY, c.[NextPayment], GETUTCDATE())
        ELSE 0
    END AS [DaysOverdue],
    c.[CreatedAt],
    c.[NextPayment],
    c.[ClosedAt]
FROM [dbo].[Credits] c
LEFT JOIN [dbo].[BillingNumbers] bn ON c.[BillingNumberId] = bn.[Id]
LEFT JOIN [dbo].[Customers] cu ON bn.[CustomerId] = cu.[Id]
ORDER BY 
    c.[IsClosed], c.[RemainingToPay] DESC;
