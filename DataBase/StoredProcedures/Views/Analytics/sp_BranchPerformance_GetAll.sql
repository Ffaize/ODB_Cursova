CREATE PROCEDURE [dbo].[sp_BranchPerformance_GetAll]
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT *
    FROM [dbo].[v_BranchPerformance]
    ORDER BY [BranchName];
END;
