CREATE TRIGGER [dbo].[tr_Employees_Insert]
ON [dbo].[Employees]
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO [dbo].[ActionsLog] ([Id], [Description], [Operation], [CreatedBy])
    SELECT 
        NEWID(),
        'Inserted into Employees: Id=' + CAST(i.[Id] AS NVARCHAR(36)) + 
        ', Name=' + ISNULL(i.[Name], 'NULL') + 
        ', Email=' + ISNULL(i.[Email], 'NULL') +
        ', Role=' + CAST(i.[Role] AS NVARCHAR(10)) +
        ', Salary=' + CAST(i.[Salary] AS NVARCHAR(20)),
        'INSERT',
        SYSTEM_USER
    FROM inserted i;
END;
