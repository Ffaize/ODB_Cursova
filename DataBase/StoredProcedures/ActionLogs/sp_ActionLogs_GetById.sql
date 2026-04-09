CREATE PROCEDURE [dbo].[sp_ActionLogs_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SELECT [Id], [Description], [Operation], [CreatedAt], [CreatedBy]
    FROM [dbo].[ActionsLog]
    WHERE [Id] = @Id
END
