CREATE PROCEDURE [dbo].[sp_Customers_Add]
    @Id UNIQUEIDENTIFIER,
    @Name NVARCHAR(MAX),
    @Surname NVARCHAR(MAX),
    @Email NVARCHAR(MAX),
    @PasswordHash NVARCHAR(MAX),
    @CreatedAt DATETIME2 = NULL
AS
BEGIN
    INSERT INTO [dbo].[Customers] ([Id], [Name], [Surname], [Email], [PasswordHash], [CreatedAt])
    VALUES (@Id, @Name, @Surname, @Email, @PasswordHash, ISNULL(@CreatedAt, GETUTCDATE()))
END
