CREATE PROCEDURE [dbo].[sp_TransactionHistory_GetAll]
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        [Id],
        [TransactionDate],
        [CustomerName],
        [TransactionType],
        [Amount],
        [Currency],
        [Description],
        [OperationType]
    FROM [dbo].[v_TransactionHistory]
    ORDER BY [TransactionDate] DESC;
END
