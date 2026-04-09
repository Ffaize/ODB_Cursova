CREATE PROCEDURE [dbo].[sp_TransactionHistory_GetLargeTransactions]
    @MinimumAmount DECIMAL(18, 2)
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
    WHERE [Amount] >= @MinimumAmount
    ORDER BY [Amount] DESC, [TransactionDate] DESC;
END
