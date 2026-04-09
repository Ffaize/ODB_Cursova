CREATE PROCEDURE [dbo].[sp_Credits_Add]
    @Id UNIQUEIDENTIFIER,
    @FullAmount DECIMAL(18, 2),
    @RemainingToPay DECIMAL(18, 2),
    @MonthlyPayment DECIMAL(18, 2),
    @DurationInMonths INT,
    @Currency NVARCHAR(3),
    @CreatedAt DATETIME2,
    @NextPayment DATETIME2,
    @LastPayment DATETIME2 = NULL,
    @ClosedAt DATETIME2 = NULL,
    @IsClosed BIT,
    @BillingNumberId UNIQUEIDENTIFIER
AS
BEGIN
    INSERT INTO [dbo].[Credits] ([Id], [FullAmount], [RemainingToPay], [MonthlyPayment], [DurationInMonths], [Currency], [CreatedAt], [NextPayment], [LastPayment], [ClosedAt], [IsClosed], [BillingNumberId])
    VALUES (@Id, @FullAmount, @RemainingToPay, @MonthlyPayment, @DurationInMonths, @Currency, @CreatedAt, @NextPayment, @LastPayment, @ClosedAt, @IsClosed, @BillingNumberId)
END
