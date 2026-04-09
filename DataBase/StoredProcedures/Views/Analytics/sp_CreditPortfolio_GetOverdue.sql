CREATE PROCEDURE [dbo].[sp_CreditPortfolio_GetOverdue]
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT *
    FROM [dbo].[v_CreditPortfolioAnalysis]
    WHERE [DaysOverdue] > 0
    ORDER BY [DaysOverdue] DESC, [RemainingToPay] DESC;
END;
