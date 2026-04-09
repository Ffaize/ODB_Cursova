CREATE PROCEDURE [dbo].[sp_CustomerAccounts_GetByCustomerId]
    @CustomerId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT *
    FROM [dbo].[v_CustomerAccountsSummary]
    WHERE [Id] = @CustomerId;
END;
