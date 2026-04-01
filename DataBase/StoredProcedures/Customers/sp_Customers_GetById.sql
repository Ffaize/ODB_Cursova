CREATE PROCEDURE [dbo].[sp_Customers_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SELECT [Id], [Name], [Surname], [Email], [PasswordHash], [CreatedAt]
    FROM [dbo].[Customers]
    WHERE [Id] = @Id
END
