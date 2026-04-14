CREATE PROCEDURE [dbo].[sp_BillingOperations_TransferBetweenAccounts]
    @FromBillingNumberId UNIQUEIDENTIFIER,
    @ToBillingNumberId UNIQUEIDENTIFIER,
    @Amount DECIMAL(18, 2)
AS
BEGIN
    BEGIN TRANSACTION;
    BEGIN TRY
        -- Валідація
        IF @Amount <= 0
        BEGIN
            RAISERROR('Amount must be greater than 0', 16, 1);
        END

        IF NOT EXISTS (SELECT 1 FROM [dbo].[BillingNumbers] WHERE [Id] = @FromBillingNumberId)
        BEGIN
            RAISERROR('Source account not found', 16, 1);
        END

        IF NOT EXISTS (SELECT 1 FROM [dbo].[BillingNumbers] WHERE [Id] = @ToBillingNumberId)
        BEGIN
            RAISERROR('Destination account not found', 16, 1);
        END

        DECLARE @FromBalance DECIMAL(18, 2);
        SELECT @FromBalance = [Balance] FROM [dbo].[BillingNumbers] WHERE [Id] = @FromBillingNumberId;

        IF @FromBalance < @Amount
        BEGIN
            RAISERROR('Insufficient balance', 16, 1);
        END

        DECLARE @TransactionId UNIQUEIDENTIFIER = NEWID();

        -- Зміна балансів
        UPDATE [dbo].[BillingNumbers] SET [Balance] = [Balance] - @Amount, [UpdatedAt] = GETUTCDATE() WHERE [Id] = @FromBillingNumberId;
        UPDATE [dbo].[BillingNumbers] SET [Balance] = [Balance] + @Amount, [UpdatedAt] = GETUTCDATE() WHERE [Id] = @ToBillingNumberId;

        -- Логування операцій
        INSERT INTO [dbo].[BillingOperations] ([Id], [Amount], [OperationType], [CreatedAt], [BillingNumberId])
        VALUES (@TransactionId, @Amount, 1, GETUTCDATE(), @FromBillingNumberId);

        INSERT INTO [dbo].[BillingOperations] ([Id], [Amount], [OperationType], [CreatedAt], [BillingNumberId])
        VALUES (NEWID(), @Amount, 1, GETUTCDATE(), @ToBillingNumberId);

        SELECT
            @TransactionId AS [TransactionId],
            'Success' AS [Status],
            GETUTCDATE() AS [Timestamp];

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
