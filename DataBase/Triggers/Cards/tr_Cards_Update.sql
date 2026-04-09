CREATE TRIGGER [dbo].[tr_Cards_Update]
ON [dbo].[Cards]
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO [dbo].[ActionsLog] ([Id], [Description], [Operation], [CreatedBy])
    SELECT 
        NEWID(),
        'Updated Cards (Id=' + CAST(i.[Id] AS NVARCHAR(36)) + '): ' +
        CASE WHEN i.[Status] <> d.[Status] THEN 'Status (' + CAST(d.[Status] AS NVARCHAR(10)) + ' → ' + CAST(i.[Status] AS NVARCHAR(10)) + '), ' ELSE '' END +
        CASE WHEN i.[CardHolderName] <> d.[CardHolderName] THEN 'CardHolderName (' + ISNULL(d.[CardHolderName], 'NULL') + ' → ' + ISNULL(i.[CardHolderName], 'NULL') + '), ' ELSE '' END +
        CASE WHEN i.[ExpirationDate] <> d.[ExpirationDate] THEN 'ExpirationDate (changed), ' ELSE '' END +
        CASE WHEN i.[cvv] <> d.[cvv] THEN 'CVV (changed), ' ELSE '' END,
        'UPDATE',
        SYSTEM_USER
    FROM inserted i
    INNER JOIN deleted d ON i.[Id] = d.[Id]
    WHERE i.[Status] <> d.[Status] 
       OR i.[CardHolderName] <> d.[CardHolderName] 
       OR i.[ExpirationDate] <> d.[ExpirationDate] 
       OR i.[cvv] <> d.[cvv];
END;
