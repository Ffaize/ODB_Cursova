CREATE PROCEDURE [dbo].[sp_BranchPerformance_GetByBranchId]
    @BranchId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT *
    FROM [dbo].[v_BranchPerformance]
    WHERE [Id] = @BranchId;
END;
