CREATE PROCEDURE [dbo].[sp_Cards_IssueCard]
    @BillingNumberId UNIQUEIDENTIFIER,
    @CardType INT
AS
BEGIN
    BEGIN TRANSACTION;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM [dbo].[BillingNumbers] WHERE [Id] = @BillingNumberId)
        BEGIN
            RAISERROR('Billing number not found', 16, 1);
        END

        DECLARE @CardId UNIQUEIDENTIFIER;
        DECLARE @CardNumber NVARCHAR(19);
        DECLARE @ExpiryDate DATETIME2;

        SET @CardId = NEWID();
        SET @CardNumber = CAST(ABS(CAST(HASHBYTES('SHA1', NEWID()) AS BIGINT)) % 9000000000000000 + 1000000000000000 AS NVARCHAR(19));
        SET @ExpiryDate = DATEADD(YEAR, 4, GETUTCDATE());

        INSERT INTO [dbo].[Cards]
            ([Id], [CardNumber], [CardType], [Status], [ExpiryDate], [CreatedAt], [UpdatedAt], [BillingNumberId])
        VALUES
            (@CardId, @CardNumber, @CardType, 1, @ExpiryDate, GETUTCDATE(), GETUTCDATE(), @BillingNumberId);

        SELECT
            @CardId AS [CardId],
            @CardNumber AS [CardNumber],
            @ExpiryDate AS [ExpiryDate],
            @CardType AS [CardType];

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
