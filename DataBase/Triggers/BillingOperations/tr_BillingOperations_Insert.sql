CREATE TRIGGER [dbo].[tr_BillingOperations_Insert]
ON [dbo].[BillingOperations]
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO [dbo].[ActionsLog] ([Id], [Description], [Operation], [CreatedBy])
    SELECT 
        NEWID(),
        'Inserted into BillingOperations: Id=' + CAST(i.[Id] AS NVARCHAR(36)) + 
        ', Amount=' + CAST(i.[Amount] AS NVARCHAR(20)) + 
        ', Currency=' + ISNULL(i.[Currency], 'NULL') +
        ', PaymentPurpose=' + CAST(i.[PaymentPurpose] AS NVARCHAR(10)),
        'INSERT',
        SYSTEM_USER
    FROM inserted i;
END;
