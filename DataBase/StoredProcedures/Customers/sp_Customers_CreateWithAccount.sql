CREATE PROCEDURE [dbo].[sp_Customers_CreateWithAccount]
    @Name NVARCHAR(MAX),
    @Surname NVARCHAR(MAX),
    @Email NVARCHAR(MAX),
    @PasswordHash NVARCHAR(MAX)
AS
BEGIN
    BEGIN TRANSACTION;
    BEGIN TRY
        DECLARE @CustomerId UNIQUEIDENTIFIER;
        DECLARE @BillingNumberId UNIQUEIDENTIFIER;
        DECLARE @AccountNumber NVARCHAR(50);

        -- Перевіримо унікальність email
        IF EXISTS (SELECT 1 FROM [dbo].[Customers] WHERE [Email] = @Email)
        BEGIN
            RAISERROR('Email already exists', 16, 1);
        END

        -- Створюємо нового клієнта
        SET @CustomerId = NEWID();
        INSERT INTO [dbo].[Customers] ([Id], [Name], [Surname], [Email], [PasswordHash], [CreatedAt])
        VALUES (@CustomerId, @Name, @Surname, @Email, @PasswordHash, GETUTCDATE());

        -- Генеруємо номер рахунку
        SET @AccountNumber = 'ACC-' + CAST(@CustomerId AS NVARCHAR(36));
        SET @BillingNumberId = NEWID();

        -- Створюємо основний рахунок (поточний, UAH, баланс 0)
        INSERT INTO [dbo].[BillingNumbers] 
            ([Id], [AccountNumber], [Balance], [Currency], [AccountType], [Status], [CreatedAt], [UpdatedAt], [CustomerId])
        VALUES 
            (@BillingNumberId, @AccountNumber, 0, 'UAH', 1, 1, GETUTCDATE(), GETUTCDATE(), @CustomerId);

        -- Повертаємо результат
        SELECT 
            @CustomerId AS [CustomerId],
            @BillingNumberId AS [BillingNumberId],
            @AccountNumber AS [AccountNumber],
            @Name AS [Name],
            @Surname AS [Surname],
            @Email AS [Email],
            CAST(0 AS DECIMAL(18, 2)) AS [Balance],
            'UAH' AS [Currency];

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
