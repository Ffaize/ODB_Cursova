CREATE PROCEDURE [dbo].[sp_Credits_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SELECT [Id], [FullAmount], [RemainingToPay], [MonthlyPayment], [DurationInMonths], [Currency], [CreatedAt], [NextPayment], [LastPayment], [ClosedAt], [IsClosed], [BillingNumberId]
    FROM [dbo].[Credits]
    WHERE [Id] = @Id
END
