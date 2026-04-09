CREATE TRIGGER [dbo].[tr_Employees_Delete]
ON [dbo].[Employees]
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO [dbo].[ActionsLog] ([Id], [Description], [Operation], [CreatedBy])
    SELECT 
        NEWID(),
        'Deleted from Employees: Id=' + CAST(d.[Id] AS NVARCHAR(36)) + 
        ', Name=' + ISNULL(d.[Name], 'NULL') + 
        ', Email=' + ISNULL(d.[Email], 'NULL') +
        ', Role=' + CAST(d.[Role] AS NVARCHAR(10)),
        'DELETE',
        SYSTEM_USER
    FROM deleted d;
END;
