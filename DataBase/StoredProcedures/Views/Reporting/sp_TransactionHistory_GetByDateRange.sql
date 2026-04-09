CREATE PROCEDURE [dbo].[sp_TransactionHistory_GetByDateRange]
    @StartDate DATETIME2,
    @EndDate DATETIME2
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
    WHERE [TransactionDate] >= @StartDate 
        AND [TransactionDate] < DATEADD(DAY, 1, @EndDate)
    ORDER BY [TransactionDate] DESC;
END
