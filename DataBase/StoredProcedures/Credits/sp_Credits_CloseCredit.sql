CREATE PROCEDURE [dbo].[sp_Credits_CloseCredit]
    @CreditId UNIQUEIDENTIFIER
AS
BEGIN
    BEGIN TRANSACTION;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM [dbo].[Credits] WHERE [Id] = @CreditId)
        BEGIN
            RAISERROR('Credit not found', 16, 1);
        END

        DECLARE @RemainingToPay DECIMAL(18, 2);
        SELECT @RemainingToPay = [RemainingToPay] FROM [dbo].[Credits] WHERE [Id] = @CreditId;

        IF @RemainingToPay > 0
        BEGIN
            RAISERROR('Credit cannot be closed - remaining balance must be zero', 16, 1);
        END

        UPDATE [dbo].[Credits]
        SET [IsClosed] = 1, [ClosedAt] = GETUTCDATE()
        WHERE [Id] = @CreditId;

        SELECT 1 AS [Success];
        
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
