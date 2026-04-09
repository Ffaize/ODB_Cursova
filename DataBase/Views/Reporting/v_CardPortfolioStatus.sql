CREATE VIEW [dbo].[v_CardPortfolioStatus]
AS
SELECT 
    ca.[Id],
    c.[Name] + ' ' + c.[Surname] AS [CardholderName],
    RIGHT(ca.[CardNumber], 4) AS [CardNumberLast4],
    ca.[CardNumber],
    CASE 
        WHEN ca.[Status] = 1 THEN 'Active'
        WHEN ca.[Status] = 2 THEN 'Expired'
        WHEN ca.[Status] = 3 THEN 'Blocked'
        ELSE 'Unknown'
    END AS [CardStatus],
    ca.[CardHolderName],
    ca.[LaunchDate],
    ca.[ExpirationDate],
    DATEDIFF(DAY, GETUTCDATE(), ca.[ExpirationDate]) AS [DaysUntilExpiration],
    CASE 
        WHEN DATEDIFF(DAY, GETUTCDATE(), ca.[ExpirationDate]) < 0 THEN 'Expired'
        WHEN DATEDIFF(DAY, GETUTCDATE(), ca.[ExpirationDate]) < 30 THEN 'Expiring Soon'
        ELSE 'Valid'
    END AS [ExpirationStatus],
    ca.[cvv],
    bn.[AccountNumber],
    bn.[Balance],
    bn.[Currency]
FROM [dbo].[Cards] ca
LEFT JOIN [dbo].[Customers] c ON ca.[CustomerId] = c.[Id]
LEFT JOIN [dbo].[BillingNumbers] bn ON ca.[BillingNumberId] = bn.[Id]
ORDER BY 
    c.[Name], c.[Surname], ca.[ExpirationDate];
