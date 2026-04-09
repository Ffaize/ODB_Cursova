CREATE VIEW [dbo].[v_BranchPerformance]
AS
SELECT 
    b.[Id],
    b.[Name] AS [BranchName],
    b.[Location],
    COUNT(DISTINCT e.[Id]) AS [EmployeeCount],
    COUNT(DISTINCT c.[Id]) AS [CustomerCount],
    ISNULL(SUM(bn.[Balance]), 0) AS [TotalBalance],
    COUNT(DISTINCT cr.[Id]) AS [ActiveCredits],
    ISNULL(AVG(e.[Salary]), 0) AS [AvgSalary],
    ISNULL(SUM(e.[Salary]), 0) AS [TotalSalaryExpense],
    COUNT(DISTINCT bo.[Id]) AS [RecentTransactions],
    b.[CreatedAt]
FROM [dbo].[Branches] b
LEFT JOIN [dbo].[Employees] e ON b.[Id] = e.[BranchId]
LEFT JOIN [dbo].[Customers] c ON e.[Id] IN (SELECT [CreatedBy] FROM [dbo].[ActionsLog] WHERE CONVERT(NVARCHAR(36), c.[Id]) = [CreatedBy])
LEFT JOIN [dbo].[BillingNumbers] bn ON c.[Id] = bn.[CustomerId]
LEFT JOIN [dbo].[Credits] cr ON bn.[Id] = cr.[BillingNumberId] AND cr.[IsClosed] = 0
LEFT JOIN [dbo].[BillingOperations] bo ON (bo.[CustomerId] = c.[Id] OR bo.[BillingNumberIdFrom] = bn.[Id]) AND bo.[CreatedAt] > DATEADD(MONTH, -1, GETUTCDATE())
GROUP BY 
    b.[Id], b.[Name], b.[Location], b.[CreatedAt]
ORDER BY 
    b.[Name];
