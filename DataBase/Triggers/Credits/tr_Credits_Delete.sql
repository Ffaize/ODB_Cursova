CREATE TRIGGER [dbo].[tr_Credits_Delete]
ON [dbo].[Credits]
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO [dbo].[ActionsLog] ([Id], [Description], [Operation], [CreatedBy])
    SELECT 
        NEWID(),
        'Deleted from Credits: Id=' + CAST(d.[Id] AS NVARCHAR(36)) + 
        ', FullAmount=' + CAST(d.[FullAmount] AS NVARCHAR(20)) + 
        ', Currency=' + ISNULL(d.[Currency], 'NULL'),
        'DELETE',
        SYSTEM_USER
    FROM deleted d;
END;
