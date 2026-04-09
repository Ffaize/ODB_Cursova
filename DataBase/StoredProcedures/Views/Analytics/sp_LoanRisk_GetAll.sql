CREATE PROCEDURE [dbo].[sp_LoanRisk_GetAll]
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT *
    FROM [dbo].[v_CustomerLoanRiskAssessment]
    ORDER BY [RiskLevel] DESC, [DebtToLoanRatio] DESC;
END;
