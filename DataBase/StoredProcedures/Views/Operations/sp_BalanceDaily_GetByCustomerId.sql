CREATE PROCEDURE [dbo].[sp_BalanceDaily_GetByCustomerId]
    @CustomerId UNIQUEIDENTIFIER
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
    WHERE [Id] = @CustomerId
    ORDER BY [AccountNumber];
END;
