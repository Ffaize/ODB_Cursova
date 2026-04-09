CREATE TRIGGER [dbo].[tr_BillingOperations_Update]
ON [dbo].[BillingOperations]
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO [dbo].[ActionsLog] ([Id], [Description], [Operation], [CreatedBy])
    SELECT 
        NEWID(),
        'Updated BillingOperations (Id=' + CAST(i.[Id] AS NVARCHAR(36)) + '): ' +
        CASE WHEN i.[Amount] <> d.[Amount] THEN 'Amount (' + CAST(d.[Amount] AS NVARCHAR(20)) + ' → ' + CAST(i.[Amount] AS NVARCHAR(20)) + '), ' ELSE '' END +
        CASE WHEN i.[Description] <> d.[Description] THEN 'Description (changed), ' ELSE '' END +
        CASE WHEN i.[PaymentPurpose] <> d.[PaymentPurpose] THEN 'PaymentPurpose (' + CAST(d.[PaymentPurpose] AS NVARCHAR(10)) + ' → ' + CAST(i.[PaymentPurpose] AS NVARCHAR(10)) + '), ' ELSE '' END,
        'UPDATE',
        SYSTEM_USER
    FROM inserted i
    INNER JOIN deleted d ON i.[Id] = d.[Id]
    WHERE i.[Amount] <> d.[Amount] 
       OR ISNULL(i.[Description], '') <> ISNULL(d.[Description], '')
       OR i.[PaymentPurpose] <> d.[PaymentPurpose];
END;
