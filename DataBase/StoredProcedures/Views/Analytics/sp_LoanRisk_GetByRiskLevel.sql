CREATE PROCEDURE [dbo].[sp_LoanRisk_GetByRiskLevel]
    @RiskLevel NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT *
    FROM [dbo].[v_CustomerLoanRiskAssessment]
    WHERE [RiskLevel] = @RiskLevel
    ORDER BY [DebtToLoanRatio] DESC;
END;
