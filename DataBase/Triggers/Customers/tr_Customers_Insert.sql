CREATE TRIGGER [dbo].[tr_Customers_Insert]
ON [dbo].[Customers]
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO [dbo].[ActionsLog] ([Id], [Description], [Operation], [CreatedBy])
    SELECT 
        NEWID(),
        'Inserted into Customers: Id=' + CAST(i.[Id] AS NVARCHAR(36)) + 
        ', Name=' + ISNULL(i.[Name], 'NULL') + 
        ', Email=' + ISNULL(i.[Email], 'NULL'),
        'INSERT',
        SYSTEM_USER
    FROM inserted i;
END;
