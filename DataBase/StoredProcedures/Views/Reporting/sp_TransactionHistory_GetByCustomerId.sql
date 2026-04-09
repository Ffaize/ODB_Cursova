CREATE PROCEDURE [dbo].[sp_TransactionHistory_GetByCustomerId]
    @CustomerId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        th.[Id],
        th.[TransactionDate],
        th.[CustomerName],
        th.[TransactionType],
        th.[Amount],
        th.[Currency],
        th.[Description],
        th.[OperationType]
    FROM [dbo].[v_TransactionHistory] th
    INNER JOIN [dbo].[BillingOperations] bo ON th.[Id] = bo.[Id]
    WHERE bo.[CustomerId] = @CustomerId
    ORDER BY th.[TransactionDate] DESC;
END
