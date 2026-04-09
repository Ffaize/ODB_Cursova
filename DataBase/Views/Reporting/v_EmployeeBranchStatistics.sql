CREATE VIEW [dbo].[v_EmployeeBranchStatistics]
AS
SELECT 
    b.[Id] AS [BranchId],
    b.[Name] AS [BranchName],
    b.[Location],
    COUNT(DISTINCT e.[Id]) AS [EmployeeCount],
    COUNT(CASE WHEN UPPER(e.[Email]) LIKE '%MANAGER%' THEN 1 END) AS [ManagerCount],
    ISNULL(AVG(e.[Salary]), 0) AS [AverageSalary],
    ISNULL(MIN(e.[Salary]), 0) AS [MinSalary],
    ISNULL(MAX(e.[Salary]), 0) AS [MaxSalary],
    ISNULL(SUM(e.[Salary]), 0) AS [TotalSalaryExpense],
    COUNT(DISTINCT e.[Id]) AS [TotalStaff],
    ISNULL(SUM(e.[Salary]) / NULLIF(COUNT(DISTINCT e.[Id]), 0), 0) AS [AverageSalaryPerEmployee],
    b.[CreatedAt]
FROM [dbo].[Branches] b
LEFT JOIN [dbo].[Employees] e ON b.[Id] = e.[BranchId]
GROUP BY 
    b.[Id], b.[Name], b.[Location], b.[CreatedAt]
ORDER BY 
    b.[Name];
