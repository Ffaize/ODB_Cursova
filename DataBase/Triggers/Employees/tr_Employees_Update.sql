CREATE TRIGGER [dbo].[tr_Employees_Update]
ON [dbo].[Employees]
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO [dbo].[ActionsLog] ([Id], [Description], [Operation], [CreatedBy])
    SELECT 
        NEWID(),
        'Updated Employees (Id=' + CAST(i.[Id] AS NVARCHAR(36)) + '): ' +
        CASE WHEN i.[Name] <> d.[Name] THEN 'Name (' + ISNULL(d.[Name], 'NULL') + ' → ' + ISNULL(i.[Name], 'NULL') + '), ' ELSE '' END +
        CASE WHEN i.[Email] <> d.[Email] THEN 'Email (' + ISNULL(d.[Email], 'NULL') + ' → ' + ISNULL(i.[Email], 'NULL') + '), ' ELSE '' END +
        CASE WHEN i.[Role] <> d.[Role] THEN 'Role (' + CAST(d.[Role] AS NVARCHAR(10)) + ' → ' + CAST(i.[Role] AS NVARCHAR(10)) + '), ' ELSE '' END +
        CASE WHEN i.[Salary] <> d.[Salary] THEN 'Salary (' + CAST(d.[Salary] AS NVARCHAR(20)) + ' → ' + CAST(i.[Salary] AS NVARCHAR(20)) + '), ' ELSE '' END,
        'UPDATE',
        SYSTEM_USER
    FROM inserted i
    INNER JOIN deleted d ON i.[Id] = d.[Id]
    WHERE i.[Name] <> d.[Name] 
       OR i.[Email] <> d.[Email] 
       OR i.[Role] <> d.[Role] 
       OR i.[Salary] <> d.[Salary];
END;
