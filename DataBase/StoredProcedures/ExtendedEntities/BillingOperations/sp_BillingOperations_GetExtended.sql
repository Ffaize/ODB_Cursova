CREATE PROCEDURE [dbo].[sp_BillingOperations_GetExtended]
AS
BEGIN
    SELECT 
        bo.[Id], bo.[Amount], bo.[Currency], bo.[CreatedAt], bo.[Description], bo.[PaymentPurpose], 
        bo.[CustomerId], bo.[BillingNumberIdFrom], bo.[BillingNumberIdTo], bo.[CreditId],
        bnf.[Id] AS BillingNumberIdFrom, bnf.[AccountNumber] AS BNFromAccountNumber, 
        bnf.[Balance] AS BNFromBalance, bnf.[Currency] AS BNFromCurrency, bnf.[Status] AS BNFromStatus,
        bnt.[Id] AS BillingNumberIdTo, bnt.[AccountNumber] AS BNToAccountNumber, 
        bnt.[Balance] AS BNToBalance, bnt.[Currency] AS BNToCurrency, bnt.[Status] AS BNToStatus,
        cr.[Id] AS CreditId, cr.[FullAmount], cr.[RemainingToPay], cr.[MonthlyPayment], 
        cr.[DurationInMonths], cr.[Currency] AS CreditCurrency, cr.[IsClosed]
    FROM [dbo].[BillingOperations] bo
    LEFT JOIN [dbo].[BillingNumbers] bnf ON bo.[BillingNumberIdFrom] = bnf.[Id]
    LEFT JOIN [dbo].[BillingNumbers] bnt ON bo.[BillingNumberIdTo] = bnt.[Id]
    LEFT JOIN [dbo].[Credits] cr ON bo.[CreditId] = cr.[Id]
    ORDER BY bo.[CreatedAt] DESC
END
