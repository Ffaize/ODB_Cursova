CREATE PROCEDURE [dbo].[sp_Cards_Delete]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    DELETE FROM [dbo].[Cards]
    WHERE [Id] = @Id
END
