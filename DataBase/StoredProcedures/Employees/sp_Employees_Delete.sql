CREATE PROCEDURE [dbo].[sp_Employees_Delete]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    DELETE FROM [dbo].[Employees]
    WHERE [Id] = @Id
END
