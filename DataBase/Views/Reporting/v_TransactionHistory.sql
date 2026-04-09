CREATE VIEW [dbo].[v_TransactionHistory]
AS
SELECT TOP (100) PERCENT
    bo.[Id],
    bo.[CreatedAt] AS [TransactionDate],
    c.[Name] + ' ' + c.[Surname] AS [CustomerName],
    CASE 
        WHEN bo.[PaymentPurpose] = 1 THEN 'Salary'
        WHEN bo.[PaymentPurpose] = 2 THEN 'Loan Payment'
        WHEN bo.[PaymentPurpose] = 3 THEN 'Transfer'
        WHEN bo.[PaymentPurpose] = 4 THEN 'Utility Bill'
        WHEN bo.[PaymentPurpose] = 5 THEN 'Deposit'
        ELSE 'Other'
    END AS [TransactionType],
    bo.[Amount],
    bo.[Currency],
    ISNULL(bo.[Description], 'N/A') AS [Description],
    CASE 
        WHEN bo.[BillingNumberIdFrom] IS NOT NULL AND bo.[BillingNumberIdTo] IS NOT NULL THEN 'Transfer Between Accounts'
        WHEN bo.[CreditId] IS NOT NULL THEN 'Credit Payment'
        ELSE 'General Operation'
    END AS [OperationType]
FROM [dbo].[BillingOperations] bo
LEFT JOIN [dbo].[Customers] c ON bo.[CustomerId] = c.[Id]
ORDER BY 
    bo.[CreatedAt] DESC;
