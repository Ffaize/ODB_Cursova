CREATE PROCEDURE [dbo].[sp_BranchPerformance_GetTopPerformers]
    @TopCount INT = 10
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT TOP (@TopCount) *
    FROM [dbo].[v_BranchPerformance]
    ORDER BY [TotalBalance] DESC, [EmployeeCount] DESC;
END;
