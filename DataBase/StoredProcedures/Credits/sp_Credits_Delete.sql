CREATE PROCEDURE [dbo].[sp_Credits_Delete]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    DELETE FROM [dbo].[Credits]
    WHERE [Id] = @Id
END
