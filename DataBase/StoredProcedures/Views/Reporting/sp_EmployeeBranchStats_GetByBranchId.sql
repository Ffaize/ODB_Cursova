CREATE PROCEDURE [dbo].[sp_EmployeeBranchStats_GetByBranchId]
    @BranchId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        [BranchId],
        [BranchName],
        [Location],
        [EmployeeCount],
        [ManagerCount],
        [AverageSalary],
        [MinSalary],
        [MaxSalary],
        [TotalSalaryExpense],
        [TotalStaff],
        [AverageSalaryPerEmployee],
        [CreatedAt]
    FROM [dbo].[v_EmployeeBranchStatistics]
    WHERE [BranchId] = @BranchId;
END
