CREATE PROCEDURE [dbo].[sp_CreditPortfolio_GetByCreditStatus]
    @Status NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT *
    FROM [dbo].[v_CreditPortfolioAnalysis]
    WHERE [Status] = @Status
    ORDER BY [RemainingToPay] DESC;
END;
