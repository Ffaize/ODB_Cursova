CREATE VIEW [dbo].[v_CustomerBalanceDaily]
AS
SELECT TOP (100) PERCENT
    c.[Id],
    c.[Name] + ' ' + c.[Surname] AS [CustomerName],
    bn.[Id] AS [AccountId],
    bn.[AccountNumber],
    bn.[Balance],
    bn.[Currency],
    bn.[Status],
    CASE 
        WHEN bn.[Status] = 1 THEN 'Active'
        WHEN bn.[Status] = 2 THEN 'Inactive'
        WHEN bn.[Status] = 3 THEN 'Frozen'
        WHEN bn.[Status] = 4 THEN 'Closed'
        WHEN bn.[Status] = 5 THEN 'Suspended'
        ELSE 'Unknown'
    END AS [StatusName],
    (SELECT COUNT(*) FROM [dbo].[BillingOperations] WHERE [BillingNumberIdFrom] = bn.[Id] AND [CreatedAt] > DATEADD(DAY, -1, GETUTCDATE())) AS [OperationsLast24H],
    (SELECT COUNT(*) FROM [dbo].[BillingOperations] WHERE [BillingNumberIdFrom] = bn.[Id] AND [CreatedAt] > DATEADD(DAY, -7, GETUTCDATE())) AS [OperationsLast7D],
    (SELECT ISNULL(SUM([Amount]), 0) FROM [dbo].[BillingOperations] WHERE [BillingNumberIdFrom] = bn.[Id] AND [CreatedAt] > DATEADD(DAY, -1, GETUTCDATE())) AS [OutflowLast24H],
    (SELECT ISNULL(SUM([Amount]), 0) FROM [dbo].[BillingOperations] WHERE [BillingNumberIdTo] = bn.[Id] AND [CreatedAt] > DATEADD(DAY, -1, GETUTCDATE())) AS [InflowLast24H],
    bn.[CreatedAt] AS [AccountCreatedDate],
    bn.[UpdatedAt] AS [LastUpdatedDate],
    DATEDIFF(DAY, bn.[CreatedAt], GETUTCDATE()) AS [AccountAgeDays]
FROM [dbo].[Customers] c
LEFT JOIN [dbo].[BillingNumbers] bn ON c.[Id] = bn.[CustomerId]
ORDER BY 
    c.[Name], c.[Surname], bn.[AccountNumber];
