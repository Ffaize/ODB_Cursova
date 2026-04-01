CREATE PROCEDURE [dbo].[sp_Customers_Update]
    @Id UNIQUEIDENTIFIER,
    @Name NVARCHAR(MAX),
    @Surname NVARCHAR(MAX),
    @Email NVARCHAR(MAX),
    @PasswordHash NVARCHAR(MAX)
AS
BEGIN
    UPDATE [dbo].[Customers]
    SET [Name] = @Name,
        [Surname] = @Surname,
        [Email] = @Email,
        [PasswordHash] = @PasswordHash
    WHERE [Id] = @Id
END
