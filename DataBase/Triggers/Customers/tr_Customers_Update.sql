CREATE TRIGGER [dbo].[tr_Customers_Update]
ON [dbo].[Customers]
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO [dbo].[ActionsLog] ([Id], [Description], [Operation], [CreatedBy])
    SELECT 
        NEWID(),
        'Updated Customers (Id=' + CAST(i.[Id] AS NVARCHAR(36)) + '): ' +
        CASE WHEN i.[Name] <> d.[Name] THEN 'Name (' + ISNULL(d.[Name], 'NULL') + ' → ' + ISNULL(i.[Name], 'NULL') + '), ' ELSE '' END +
        CASE WHEN i.[Surname] <> d.[Surname] THEN 'Surname (' + ISNULL(d.[Surname], 'NULL') + ' → ' + ISNULL(i.[Surname], 'NULL') + '), ' ELSE '' END +
        CASE WHEN i.[Email] <> d.[Email] THEN 'Email (' + ISNULL(d.[Email], 'NULL') + ' → ' + ISNULL(i.[Email], 'NULL') + '), ' ELSE '' END +
        CASE WHEN i.[PasswordHash] <> d.[PasswordHash] THEN 'PasswordHash (changed), ' ELSE '' END,
        'UPDATE',
        SYSTEM_USER
    FROM inserted i
    INNER JOIN deleted d ON i.[Id] = d.[Id]
    WHERE i.[Name] <> d.[Name] 
       OR i.[Surname] <> d.[Surname] 
       OR i.[Email] <> d.[Email] 
       OR i.[PasswordHash] <> d.[PasswordHash];
END;
