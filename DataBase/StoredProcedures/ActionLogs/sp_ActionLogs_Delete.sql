CREATE PROCEDURE [dbo].[sp_ActionLogs_Delete]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    DELETE FROM [dbo].[ActionsLog]
    WHERE [Id] = @Id
END
