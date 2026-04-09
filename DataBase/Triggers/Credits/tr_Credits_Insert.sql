CREATE TRIGGER [dbo].[tr_Credits_Insert]
ON [dbo].[Credits]
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO [dbo].[ActionsLog] ([Id], [Description], [Operation], [CreatedBy])
    SELECT 
        NEWID(),
        'Inserted into Credits: Id=' + CAST(i.[Id] AS NVARCHAR(36)) + 
        ', FullAmount=' + CAST(i.[FullAmount] AS NVARCHAR(20)) + 
        ', Currency=' + ISNULL(i.[Currency], 'NULL') +
        ', DurationInMonths=' + CAST(i.[DurationInMonths] AS NVARCHAR(10)),
        'INSERT',
        SYSTEM_USER
    FROM inserted i;
END;
