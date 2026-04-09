CREATE TRIGGER [dbo].[tr_Customers_Delete]
ON [dbo].[Customers]
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO [dbo].[ActionsLog] ([Id], [Description], [Operation], [CreatedBy])
    SELECT 
        NEWID(),
        'Deleted from Customers: Id=' + CAST(d.[Id] AS NVARCHAR(36)) + 
        ', Name=' + ISNULL(d.[Name], 'NULL') + 
        ', Email=' + ISNULL(d.[Email], 'NULL'),
        'DELETE',
        SYSTEM_USER
    FROM deleted d;
END;
