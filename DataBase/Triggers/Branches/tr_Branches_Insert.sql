CREATE TRIGGER [dbo].[tr_Branches_Insert]
ON [dbo].[Branches]
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO [dbo].[ActionsLog] ([Id], [Description], [Operation], [CreatedBy])
    SELECT 
        NEWID(),
        'Inserted into Branches: Id=' + CAST(i.[Id] AS NVARCHAR(36)) + 
        ', Name=' + ISNULL(i.[Name], 'NULL') + 
        ', Location=' + ISNULL(i.[Location], 'NULL'),
        'INSERT',
        SYSTEM_USER
    FROM inserted i;
END;
