CREATE TRIGGER [dbo].[tr_Cards_Insert]
ON [dbo].[Cards]
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO [dbo].[ActionsLog] ([Id], [Description], [Operation], [CreatedBy])
    SELECT 
        NEWID(),
        'Inserted into Cards: Id=' + CAST(i.[Id] AS NVARCHAR(36)) + 
        ', CardNumber=' + ISNULL(i.[CardNumber], 'NULL') + 
        ', Status=' + CAST(i.[Status] AS NVARCHAR(10)) +
        ', CardHolderName=' + ISNULL(i.[CardHolderName], 'NULL'),
        'INSERT',
        SYSTEM_USER
    FROM inserted i;
END;
