CREATE PROCEDURE [dbo].[sp_Credits_OpenCredit]
    @CustomerId UNIQUEIDENTIFIER,
    @FullAmount DECIMAL(18, 2),
    @DurationInMonths INT,
    @Currency NVARCHAR(3) = 'UAH'
AS
BEGIN
    BEGIN TRANSACTION;
    BEGIN TRY
        -- Валідація
        IF @FullAmount <= 0
        BEGIN
            RAISERROR('Full amount must be greater than 0', 16, 1);
        END

        IF @DurationInMonths <= 0
        BEGIN
            RAISERROR('Duration in months must be greater than 0', 16, 1);
        END

        IF NOT EXISTS (SELECT 1 FROM [dbo].[Customers] WHERE [Id] = @CustomerId)
        BEGIN
            RAISERROR('Customer not found', 16, 1);
        END

        DECLARE @CreditId UNIQUEIDENTIFIER;
        DECLARE @BillingNumberId UNIQUEIDENTIFIER;
        DECLARE @AccountNumber NVARCHAR(50);
        DECLARE @MonthlyPayment DECIMAL(18, 2);
        DECLARE @NextPaymentDate DATETIME2;

        SET @CreditId = NEWID();
        SET @BillingNumberId = NEWID();
        SET @AccountNumber = 'CREDIT-' + CAST(@CreditId AS NVARCHAR(36));
        SET @MonthlyPayment = @FullAmount / @DurationInMonths;
        SET @NextPaymentDate = DATEADD(MONTH, 1, GETUTCDATE());

        -- Створюємо BillingNumber для кредиту
        INSERT INTO [dbo].[BillingNumbers]
            ([Id], [AccountNumber], [Balance], [Currency], [AccountType], [Status], [CreatedAt], [UpdatedAt], [CustomerId])
        VALUES
            (@BillingNumberId, @AccountNumber, @FullAmount, @Currency, 2, 1, GETUTCDATE(), GETUTCDATE(), @CustomerId);

        -- Створюємо кредит
        INSERT INTO [dbo].[Credits]
            ([Id], [FullAmount], [RemainingToPay], [MonthlyPayment], [DurationInMonths], [Currency], [CreatedAt], [NextPayment], [LastPayment], [ClosedAt], [IsClosed], [BillingNumberId])
        VALUES
            (@CreditId, @FullAmount, @FullAmount, @MonthlyPayment, @DurationInMonths, @Currency, GETUTCDATE(), @NextPaymentDate, NULL, NULL, 0, @BillingNumberId);

        -- Повертаємо результат
        SELECT
            @CreditId AS [CreditId],
            @BillingNumberId AS [BillingNumberId],
            @AccountNumber AS [AccountNumber],
            @MonthlyPayment AS [MonthlyPayment],
            @NextPaymentDate AS [FirstPaymentDate],
            @FullAmount AS [FullAmount];

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
