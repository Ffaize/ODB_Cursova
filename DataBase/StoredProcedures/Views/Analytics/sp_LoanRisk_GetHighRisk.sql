CREATE PROCEDURE [dbo].[sp_LoanRisk_GetHighRisk]
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT *
    FROM [dbo].[v_CustomerLoanRiskAssessment]
    WHERE [RiskLevel] = 'High Risk'
    ORDER BY [DebtToLoanRatio] DESC, [TotalDebt] DESC;
END;
