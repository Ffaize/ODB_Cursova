CREATE PROCEDURE [dbo].[sp_CreditPortfolio_GetByCurrency]
    @Currency NVARCHAR(3)
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT *
    FROM [dbo].[v_CreditPortfolioAnalysis]
    WHERE [Currency] = @Currency
    ORDER BY [RemainingToPay] DESC;
END;
