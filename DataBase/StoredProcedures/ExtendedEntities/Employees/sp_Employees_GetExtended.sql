CREATE PROCEDURE [dbo].[sp_Employees_GetExtended]
AS
BEGIN
    SELECT 
        e.[Id], e.[Name], e.[Surname], e.[Email], e.[PasswordHash], e.[CreatedAt],
        e.[Role], e.[BranchId], e.[Salary], e.[HiredAt],
        b.[Id] AS BranchId, b.[Name] AS BranchName, b.[NumberOfEmployees], b.[NumberOfBranch],
        b.[Location], b.[ContactEmail], b.[ContactPhone], b.[CreatedAt] AS BranchCreatedAt
    FROM [dbo].[Employees] e
    INNER JOIN [dbo].[Branches] b ON e.[BranchId] = b.[Id]
    ORDER BY e.[CreatedAt] DESC
END
