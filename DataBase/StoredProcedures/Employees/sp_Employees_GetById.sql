CREATE PROCEDURE [dbo].[sp_Employees_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SELECT [Id], [Name], [Surname], [Email], [PasswordHash], [CreatedAt], [Role], [BranchId], [Salary], [HiredAt]
    FROM [dbo].[Employees]
    WHERE [Id] = @Id
END
