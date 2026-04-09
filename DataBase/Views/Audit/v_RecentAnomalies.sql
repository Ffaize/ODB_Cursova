CREATE VIEW [dbo].[v_RecentAnomalies]
AS
SELECT TOP (100) PERCENT
    bo.[Id],
    c.[Name] + ' ' + c.[Surname] AS [CustomerName],
    bo.[Amount],
    bo.[Currency],
    bo.[CreatedAt],
    CASE 
        WHEN bo.[Amount] > 10000 THEN 'Large Transaction'
        WHEN (
            SELECT COUNT(*) 
            FROM [dbo].[BillingOperations] bo2 
            WHERE bo2.[CustomerId] = bo.[CustomerId] 
            AND bo2.[CreatedAt] > DATEADD(DAY, -1, bo.[CreatedAt])
        ) > 5 THEN 'Frequent Activity'
        WHEN DATEPART(HOUR, bo.[CreatedAt]) BETWEEN 22 AND 5 THEN 'Unusual Time'
        WHEN bo.[Amount] > (
            SELECT AVG([Amount]) * 2 
            FROM [dbo].[BillingOperations] 
            WHERE [CustomerId] = bo.[CustomerId]
        ) THEN 'Amount Spike'
        ELSE 'Normal'
    END AS [AnomalyType],
    bo.[Description],
    bn.[Balance] AS [RemainingBalance]
FROM [dbo].[BillingOperations] bo
LEFT JOIN [dbo].[Customers] c ON bo.[CustomerId] = c.[Id]
LEFT JOIN [dbo].[BillingNumbers] bn ON bo.[BillingNumberIdFrom] = bn.[Id]
WHERE bo.[CreatedAt] > DATEADD(DAY, -7, GETUTCDATE())
AND (
    bo.[Amount] > 10000
    OR DATEPART(HOUR, bo.[CreatedAt]) BETWEEN 22 AND 5
    OR (
        SELECT COUNT(*) 
        FROM [dbo].[BillingOperations] bo2 
        WHERE bo2.[CustomerId] = bo.[CustomerId] 
        AND bo2.[CreatedAt] > DATEADD(DAY, -1, bo.[CreatedAt])
    ) > 5
)
ORDER BY 
    bo.[CreatedAt] DESC;
