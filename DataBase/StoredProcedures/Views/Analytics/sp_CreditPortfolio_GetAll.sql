CREATE PROCEDURE [dbo].[sp_CreditPortfolio_GetAll]
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT *
    FROM [dbo].[v_CreditPortfolioAnalysis]
    ORDER BY [Status], [RemainingToPay] DESC;
END;
