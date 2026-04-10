CREATE PROCEDURE [dbo].[sp_Credits_GetExtended]
AS
BEGIN
    SELECT 
        c.[Id], c.[FullAmount], c.[RemainingToPay], c.[MonthlyPayment], c.[DurationInMonths], 
        c.[Currency], c.[CreatedAt], c.[NextPayment], c.[LastPayment], c.[ClosedAt], c.[IsClosed], 
        c.[BillingNumberId],
        bn.[Id] AS BillingNumberId, bn.[AccountNumber], bn.[Balance], bn.[Currency] AS BNCurrency, 
        bn.[AccountType], bn.[Status], bn.[CreatedAt] AS BNCreatedAt, bn.[UpdatedAt], bn.[CustomerId]
    FROM [dbo].[Credits] c
    INNER JOIN [dbo].[BillingNumbers] bn ON c.[BillingNumberId] = bn.[Id]
    ORDER BY c.[CreatedAt] DESC
END
