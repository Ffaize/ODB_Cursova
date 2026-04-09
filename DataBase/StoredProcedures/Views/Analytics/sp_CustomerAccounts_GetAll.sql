CREATE PROCEDURE [dbo].[sp_CustomerAccounts_GetAll]
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT *
    FROM [dbo].[v_CustomerAccountsSummary]
    ORDER BY [Name], [Surname];
END;
