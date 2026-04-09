CREATE TRIGGER [dbo].[tr_Branches_Delete]
ON [dbo].[Branches]
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO [dbo].[ActionsLog] ([Id], [Description], [Operation], [CreatedBy])
    SELECT 
        NEWID(),
        'Deleted from Branches: Id=' + CAST(d.[Id] AS NVARCHAR(36)) + 
        ', Name=' + ISNULL(d.[Name], 'NULL') + 
        ', Location=' + ISNULL(d.[Location], 'NULL'),
        'DELETE',
        SYSTEM_USER
    FROM deleted d;
END;
