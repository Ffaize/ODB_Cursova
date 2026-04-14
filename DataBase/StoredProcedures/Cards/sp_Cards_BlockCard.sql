CREATE PROCEDURE [dbo].[sp_Cards_BlockCard]
    @CardId UNIQUEIDENTIFIER
AS
BEGIN
    BEGIN TRANSACTION;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM [dbo].[Cards] WHERE [Id] = @CardId)
        BEGIN
            RAISERROR('Card not found', 16, 1);
        END

        DECLARE @CurrentStatus INT;
        SELECT @CurrentStatus = [Status] FROM [dbo].[Cards] WHERE [Id] = @CardId;

        IF @CurrentStatus = 3
        BEGIN
            RAISERROR('Card is already blocked', 16, 1);
        END

        UPDATE [dbo].[Cards]
        SET [Status] = 3, [UpdatedAt] = GETUTCDATE()
        WHERE [Id] = @CardId;

        SELECT 1 AS [Success];
        
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
