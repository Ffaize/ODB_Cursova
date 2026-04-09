CREATE PROCEDURE [dbo].[sp_Branches_Delete]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    DELETE FROM [dbo].[Branches]
    WHERE [Id] = @Id
END
