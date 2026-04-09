CREATE PROCEDURE [dbo].[sp_ActionLogs_Update]
    @Id UNIQUEIDENTIFIER,
    @Description NVARCHAR(MAX),
    @Operation NVARCHAR(255),
    @CreatedBy NVARCHAR(255)
AS
BEGIN
    UPDATE [dbo].[ActionsLog]
    SET [Description] = @Description,
        [Operation] = @Operation,
        [CreatedBy] = @CreatedBy
    WHERE [Id] = @Id
END
