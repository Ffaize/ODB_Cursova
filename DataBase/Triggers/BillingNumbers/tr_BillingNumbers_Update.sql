CREATE TRIGGER [dbo].[tr_BillingNumbers_Update]
ON [dbo].[BillingNumbers]
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO [dbo].[ActionsLog] ([Id], [Description], [Operation], [CreatedBy])
    SELECT 
        NEWID(),
        'Updated BillingNumbers (Id=' + CAST(i.[Id] AS NVARCHAR(36)) + '): ' +
        CASE WHEN i.[Balance] <> d.[Balance] THEN 'Balance (' + CAST(d.[Balance] AS NVARCHAR(20)) + ' → ' + CAST(i.[Balance] AS NVARCHAR(20)) + '), ' ELSE '' END +
        CASE WHEN i.[AccountType] <> d.[AccountType] THEN 'AccountType (' + CAST(d.[AccountType] AS NVARCHAR(10)) + ' → ' + CAST(i.[AccountType] AS NVARCHAR(10)) + '), ' ELSE '' END +
        CASE WHEN i.[Status] <> d.[Status] THEN 'Status (' + CAST(d.[Status] AS NVARCHAR(10)) + ' → ' + CAST(i.[Status] AS NVARCHAR(10)) + '), ' ELSE '' END,
        'UPDATE',
        SYSTEM_USER
    FROM inserted i
    INNER JOIN deleted d ON i.[Id] = d.[Id]
    WHERE i.[Balance] <> d.[Balance] 
       OR i.[AccountType] <> d.[AccountType] 
       OR i.[Status] <> d.[Status];
END;
