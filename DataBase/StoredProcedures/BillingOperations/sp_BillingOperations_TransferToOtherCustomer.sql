CREATE PROCEDURE [dbo].[sp_BillingOperations_TransferToOtherCustomer]
    @FromBillingNumberId UNIQUEIDENTIFIER,
    @ToAccountNumber NVARCHAR(50),
    @Amount DECIMAL(18, 2),
    @Description NVARCHAR(MAX)
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

        DECLARE @ToBillingNumberId UNIQUEIDENTIFIER;
        SELECT @ToBillingNumberId = [Id] FROM [dbo].[BillingNumbers] WHERE [AccountNumber] = @ToAccountNumber;

        IF @ToBillingNumberId IS NULL
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

        -- Логування операцій з описом
        INSERT INTO [dbo].[BillingOperations] ([Id], [Amount], [OperationType], [CreatedAt], [BillingNumberId])
        VALUES (@TransactionId, @Amount, 3, GETUTCDATE(), @FromBillingNumberId);

        INSERT INTO [dbo].[BillingOperations] ([Id], [Amount], [OperationType], [CreatedAt], [BillingNumberId])
        VALUES (NEWID(), @Amount, 3, GETUTCDATE(), @ToBillingNumberId);

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
