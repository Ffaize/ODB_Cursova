CREATE TRIGGER [dbo].[tr_BillingNumbers_Delete]
ON [dbo].[BillingNumbers]
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO [dbo].[ActionsLog] ([Id], [Description], [Operation], [CreatedBy])
    SELECT 
        NEWID(),
        'Deleted from BillingNumbers: Id=' + CAST(d.[Id] AS NVARCHAR(36)) + 
        ', AccountNumber=' + ISNULL(d.[AccountNumber], 'NULL') + 
        ', Balance=' + CAST(d.[Balance] AS NVARCHAR(20)),
        'DELETE',
        SYSTEM_USER
    FROM deleted d;
END;
