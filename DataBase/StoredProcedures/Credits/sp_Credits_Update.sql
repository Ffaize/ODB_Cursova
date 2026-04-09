CREATE PROCEDURE [dbo].[sp_Credits_Update]
    @Id UNIQUEIDENTIFIER,
    @FullAmount DECIMAL(18, 2),
    @RemainingToPay DECIMAL(18, 2),
    @MonthlyPayment DECIMAL(18, 2),
    @DurationInMonths INT,
    @Currency NVARCHAR(3),
    @NextPayment DATETIME2,
    @LastPayment DATETIME2 = NULL,
    @ClosedAt DATETIME2 = NULL,
    @IsClosed BIT,
    @BillingNumberId UNIQUEIDENTIFIER
AS
BEGIN
    UPDATE [dbo].[Credits]
    SET [FullAmount] = @FullAmount,
        [RemainingToPay] = @RemainingToPay,
        [MonthlyPayment] = @MonthlyPayment,
        [DurationInMonths] = @DurationInMonths,
        [Currency] = @Currency,
        [NextPayment] = @NextPayment,
        [LastPayment] = @LastPayment,
        [ClosedAt] = @ClosedAt,
        [IsClosed] = @IsClosed,
        [BillingNumberId] = @BillingNumberId
    WHERE [Id] = @Id
END
