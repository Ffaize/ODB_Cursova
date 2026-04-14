CREATE PROCEDURE [dbo].[sp_Credits_GetPaymentSchedule]
    @CreditId UNIQUEIDENTIFIER
AS
BEGIN
    DECLARE @MonthlyPayment DECIMAL(18, 2);
    DECLARE @RemainingToPay DECIMAL(18, 2);
    DECLARE @DurationInMonths INT;
    DECLARE @CreatedAt DATETIME2;
    DECLARE @PaymentNumber INT = 1;

    SELECT 
        @MonthlyPayment = [MonthlyPayment],
        @RemainingToPay = [RemainingToPay],
        @DurationInMonths = [DurationInMonths],
        @CreatedAt = [CreatedAt]
    FROM [dbo].[Credits]
    WHERE [Id] = @CreditId;

    IF @CreditId IS NULL
    BEGIN
        RAISERROR('Credit not found', 16, 1);
    END

    DECLARE @Counter INT = 1;
    WHILE @Counter <= @DurationInMonths
    BEGIN
        SELECT
            @Counter AS [PaymentNumber],
            @MonthlyPayment AS [MonthlyPayment],
            DATEADD(MONTH, @Counter, @CreatedAt) AS [DueDate],
            CASE 
                WHEN @Counter <= (SELECT COUNT(*) FROM [dbo].[BillingOperations] WHERE [BillingNumberId] = (SELECT [BillingNumberId] FROM [dbo].[Credits] WHERE [Id] = @CreditId)) THEN 'Paid'
                ELSE 'Pending'
            END AS [Status],
            @RemainingToPay - (@MonthlyPayment * (@Counter - 1)) AS [RemainingAmount];
        
        SET @Counter = @Counter + 1;
    END
END
