CREATE PROCEDURE [dbo].[sp_BillingOperations_PayBilling]
    @BillingNumberId UNIQUEIDENTIFIER,
    @Amount DECIMAL(18, 2),
    @OperationType INT
AS
BEGIN
    BEGIN TRANSACTION;
    BEGIN TRY
        -- Валідація
        IF @Amount <= 0
        BEGIN
            RAISERROR('Amount must be greater than 0', 16, 1);
        END

        IF NOT EXISTS (SELECT 1 FROM [dbo].[BillingNumbers] WHERE [Id] = @BillingNumberId)
        BEGIN
            RAISERROR('Account not found', 16, 1);
        END

        DECLARE @CurrentBalance DECIMAL(18, 2);
        SELECT @CurrentBalance = [Balance] FROM [dbo].[BillingNumbers] WHERE [Id] = @BillingNumberId;

        IF @CurrentBalance < @Amount
        BEGIN
            RAISERROR('Insufficient balance', 16, 1);
        END

        DECLARE @OperationId UNIQUEIDENTIFIER = NEWID();
        DECLARE @RemainingToPay DECIMAL(18, 2) = NULL;

        -- Оновлюємо баланс
        UPDATE [dbo].[BillingNumbers] SET [Balance] = [Balance] - @Amount, [UpdatedAt] = GETUTCDATE() WHERE [Id] = @BillingNumberId;

        -- Якщо це оплата кредиту, оновлюємо Credit.RemainingToPay
        DECLARE @CreditId UNIQUEIDENTIFIER;
        SELECT @CreditId = [Id] FROM [dbo].[Credits] WHERE [BillingNumberId] = @BillingNumberId;

        IF @CreditId IS NOT NULL
        BEGIN
            UPDATE [dbo].[Credits] SET [RemainingToPay] = [RemainingToPay] - @Amount, [LastPayment] = GETUTCDATE() WHERE [Id] = @CreditId;
            SELECT @RemainingToPay = [RemainingToPay] FROM [dbo].[Credits] WHERE [Id] = @CreditId;
        END

        -- Логуємо операцію
        INSERT INTO [dbo].[BillingOperations] ([Id], [Amount], [OperationType], [CreatedAt], [BillingNumberId])
        VALUES (@OperationId, @Amount, @OperationType, GETUTCDATE(), @BillingNumberId);

        SELECT
            @OperationId AS [OperationId],
            @CurrentBalance - @Amount AS [NewBalance],
            @RemainingToPay AS [RemainingToPay];

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
