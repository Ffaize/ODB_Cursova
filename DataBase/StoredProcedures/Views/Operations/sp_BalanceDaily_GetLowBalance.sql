CREATE PROCEDURE [dbo].[sp_BalanceDaily_GetLowBalance]
    @ThresholdBalance DECIMAL(15,2) = 1000
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        [Id],
        [CustomerName],
        [AccountId],
        [AccountNumber],
        [Balance],
        [Currency],
        [Status],
        [StatusName],
        [OperationsLast24H],
        [OperationsLast7D],
        [OutflowLast24H],
        [InflowLast24H],
        [AccountCreatedDate],
        [LastUpdatedDate],
        [AccountAgeDays]
    FROM [dbo].[v_CustomerBalanceDaily]
    WHERE [Balance] <= @ThresholdBalance
    ORDER BY [Balance] ASC, [CustomerName], [AccountNumber];
END;
