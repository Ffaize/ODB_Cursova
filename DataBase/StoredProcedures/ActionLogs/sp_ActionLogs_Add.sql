CREATE PROCEDURE [dbo].[sp_ActionLogs_Add]
    @Id UNIQUEIDENTIFIER,
    @Description NVARCHAR(MAX),
    @Operation NVARCHAR(255),
    @CreatedBy NVARCHAR(255)
AS
BEGIN
    INSERT INTO [dbo].[ActionsLog] ([Id], [Description], [Operation], [CreatedAt], [CreatedBy])
    VALUES (@Id, @Description, @Operation, GETUTCDATE(), @CreatedBy)
END
