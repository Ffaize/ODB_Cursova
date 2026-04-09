CREATE PROCEDURE [dbo].[sp_Employees_Update]
    @Id UNIQUEIDENTIFIER,
    @Name NVARCHAR(MAX),
    @Surname NVARCHAR(MAX),
    @Email NVARCHAR(MAX),
    @PasswordHash NVARCHAR(MAX),
    @Role INT,
    @BranchId UNIQUEIDENTIFIER,
    @Salary DECIMAL(18, 2),
    @HiredAt DATETIME2
AS
BEGIN
    UPDATE [dbo].[Employees]
    SET [Name] = @Name,
        [Surname] = @Surname,
        [Email] = @Email,
        [PasswordHash] = @PasswordHash,
        [Role] = @Role,
        [BranchId] = @BranchId,
        [Salary] = @Salary,
        [HiredAt] = @HiredAt
    WHERE [Id] = @Id
END
