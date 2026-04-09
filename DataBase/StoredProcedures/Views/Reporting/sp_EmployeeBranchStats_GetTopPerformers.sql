CREATE PROCEDURE [dbo].[sp_EmployeeBranchStats_GetTopPerformers]
    @TopCount INT = 10
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT TOP (@TopCount)
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
    ORDER BY [EmployeeCount] DESC, [AverageSalary] DESC;
END
