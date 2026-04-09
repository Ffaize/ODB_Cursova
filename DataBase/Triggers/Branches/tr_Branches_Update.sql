CREATE TRIGGER [dbo].[tr_Branches_Update]
ON [dbo].[Branches]
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO [dbo].[ActionsLog] ([Id], [Description], [Operation], [CreatedBy])
    SELECT 
        NEWID(),
        'Updated Branches (Id=' + CAST(i.[Id] AS NVARCHAR(36)) + '): ' +
        CASE WHEN i.[Name] <> d.[Name] THEN 'Name (' + ISNULL(d.[Name], 'NULL') + ' → ' + ISNULL(i.[Name], 'NULL') + '), ' ELSE '' END +
        CASE WHEN i.[Location] <> d.[Location] THEN 'Location (' + ISNULL(d.[Location], 'NULL') + ' → ' + ISNULL(i.[Location], 'NULL') + '), ' ELSE '' END +
        CASE WHEN i.[NumberOfEmployees] <> d.[NumberOfEmployees] THEN 'NumberOfEmployees (' + CAST(d.[NumberOfEmployees] AS NVARCHAR(10)) + ' → ' + CAST(i.[NumberOfEmployees] AS NVARCHAR(10)) + '), ' ELSE '' END +
        CASE WHEN i.[ContactEmail] <> d.[ContactEmail] THEN 'ContactEmail (' + ISNULL(d.[ContactEmail], 'NULL') + ' → ' + ISNULL(i.[ContactEmail], 'NULL') + '), ' ELSE '' END,
        'UPDATE',
        SYSTEM_USER
    FROM inserted i
    INNER JOIN deleted d ON i.[Id] = d.[Id]
    WHERE i.[Name] <> d.[Name] 
       OR i.[Location] <> d.[Location] 
       OR i.[NumberOfEmployees] <> d.[NumberOfEmployees] 
       OR i.[ContactEmail] <> d.[ContactEmail];
END;
