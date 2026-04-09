CREATE PROCEDURE [dbo].[sp_EmployeeBranchStats_GetAll]
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
    ORDER BY [BranchName];
END
