CREATE TRIGGER [dbo].[tr_Credits_Update]
ON [dbo].[Credits]
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO [dbo].[ActionsLog] ([Id], [Description], [Operation], [CreatedBy])
    SELECT 
        NEWID(),
        'Updated Credits (Id=' + CAST(i.[Id] AS NVARCHAR(36)) + '): ' +
        CASE WHEN i.[RemainingToPay] <> d.[RemainingToPay] THEN 'RemainingToPay (' + CAST(d.[RemainingToPay] AS NVARCHAR(20)) + ' → ' + CAST(i.[RemainingToPay] AS NVARCHAR(20)) + '), ' ELSE '' END +
        CASE WHEN i.[IsClosed] <> d.[IsClosed] THEN 'IsClosed (' + CAST(d.[IsClosed] AS NVARCHAR(5)) + ' → ' + CAST(i.[IsClosed] AS NVARCHAR(5)) + '), ' ELSE '' END +
        CASE WHEN ISNULL(i.[LastPayment], '1900-01-01') <> ISNULL(d.[LastPayment], '1900-01-01') THEN 'LastPayment (changed), ' ELSE '' END +
        CASE WHEN ISNULL(i.[ClosedAt], '1900-01-01') <> ISNULL(d.[ClosedAt], '1900-01-01') THEN 'ClosedAt (changed), ' ELSE '' END,
        'UPDATE',
        SYSTEM_USER
    FROM inserted i
    INNER JOIN deleted d ON i.[Id] = d.[Id]
    WHERE i.[RemainingToPay] <> d.[RemainingToPay] 
       OR i.[IsClosed] <> d.[IsClosed] 
       OR ISNULL(i.[LastPayment], '1900-01-01') <> ISNULL(d.[LastPayment], '1900-01-01') 
       OR ISNULL(i.[ClosedAt], '1900-01-01') <> ISNULL(d.[ClosedAt], '1900-01-01');
END;
