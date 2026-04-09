CREATE TRIGGER [dbo].[tr_BillingOperations_Delete]
ON [dbo].[BillingOperations]
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO [dbo].[ActionsLog] ([Id], [Description], [Operation], [CreatedBy])
    SELECT 
        NEWID(),
        'Deleted from BillingOperations: Id=' + CAST(d.[Id] AS NVARCHAR(36)) + 
        ', Amount=' + CAST(d.[Amount] AS NVARCHAR(20)) + 
        ', Currency=' + ISNULL(d.[Currency], 'NULL'),
        'DELETE',
        SYSTEM_USER
    FROM deleted d;
END;
