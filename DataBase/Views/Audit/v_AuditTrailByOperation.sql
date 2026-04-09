CREATE VIEW [dbo].[v_AuditTrailByOperation]
AS
SELECT TOP (100) PERCENT
    al.[Id],
    al.[CreatedAt] AS [EventDate],
    al.[Operation],
    al.[CreatedBy] AS [User],
    CASE 
        WHEN al.[Description] LIKE '%Customers%' THEN 'Customers'
        WHEN al.[Description] LIKE '%Employees%' THEN 'Employees'
        WHEN al.[Description] LIKE '%Branches%' THEN 'Branches'
        WHEN al.[Description] LIKE '%Cards%' THEN 'Cards'
        WHEN al.[Description] LIKE '%Credits%' THEN 'Credits'
        WHEN al.[Description] LIKE '%BillingNumbers%' THEN 'BillingNumbers'
        WHEN al.[Description] LIKE '%BillingOperations%' THEN 'BillingOperations'
        ELSE 'Unknown'
    END AS [AffectedTable],
    al.[Description],
    MONTH(al.[CreatedAt]) AS [Month],
    YEAR(al.[CreatedAt]) AS [Year],
    DATEPART(WEEKDAY, al.[CreatedAt]) AS [DayOfWeek]
FROM [dbo].[ActionsLog] al
ORDER BY 
    al.[CreatedAt] DESC;
