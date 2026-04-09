CREATE TRIGGER [dbo].[tr_BillingNumbers_Insert]
ON [dbo].[BillingNumbers]
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO [dbo].[ActionsLog] ([Id], [Description], [Operation], [CreatedBy])
    SELECT 
        NEWID(),
        'Inserted into BillingNumbers: Id=' + CAST(i.[Id] AS NVARCHAR(36)) + 
        ', AccountNumber=' + ISNULL(i.[AccountNumber], 'NULL') + 
        ', Balance=' + CAST(i.[Balance] AS NVARCHAR(20)) +
        ', Currency=' + ISNULL(i.[Currency], 'NULL'),
        'INSERT',
        SYSTEM_USER
    FROM inserted i;
END;
