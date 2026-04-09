CREATE PROCEDURE [dbo].[sp_CustomerAccounts_GetHighBalance]
    @MinimumBalance DECIMAL(18,2)
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT *
    FROM [dbo].[v_CustomerAccountsSummary]
    WHERE [TotalBalance] >= @MinimumBalance
    ORDER BY [TotalBalance] DESC;
END;
