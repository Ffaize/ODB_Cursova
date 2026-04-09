CREATE PROCEDURE [dbo].[sp_Branches_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SELECT [Id], [Name], [NumberOfEmployees], [NumberOfBranch], [Location], [ContactEmail], [ContactPhone], [CreatedAt]
    FROM [dbo].[Branches]
    WHERE [Id] = @Id
END
