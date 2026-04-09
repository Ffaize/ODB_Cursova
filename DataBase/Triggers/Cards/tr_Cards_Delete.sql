CREATE TRIGGER [dbo].[tr_Cards_Delete]
ON [dbo].[Cards]
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO [dbo].[ActionsLog] ([Id], [Description], [Operation], [CreatedBy])
    SELECT 
        NEWID(),
        'Deleted from Cards: Id=' + CAST(d.[Id] AS NVARCHAR(36)) + 
        ', CardNumber=' + ISNULL(d.[CardNumber], 'NULL') + 
        ', CardHolderName=' + ISNULL(d.[CardHolderName], 'NULL'),
        'DELETE',
        SYSTEM_USER
    FROM deleted d;
END;
