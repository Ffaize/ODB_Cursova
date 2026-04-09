CREATE PROCEDURE [dbo].[sp_Employees_Add]
    @Id UNIQUEIDENTIFIER,
    @Name NVARCHAR(MAX),
    @Surname NVARCHAR(MAX),
    @Email NVARCHAR(MAX),
    @PasswordHash NVARCHAR(MAX),
    @Role INT,
    @BranchId UNIQUEIDENTIFIER,
    @Salary DECIMAL(18, 2),
    @HiredAt DATETIME2,
    @CreatedAt DATETIME2 = NULL
AS
BEGIN
    INSERT INTO [dbo].[Employees] ([Id], [Name], [Surname], [Email], [PasswordHash], [CreatedAt], [Role], [BranchId], [Salary], [HiredAt])
    VALUES (@Id, @Name, @Surname, @Email, @PasswordHash, ISNULL(@CreatedAt, GETUTCDATE()), @Role, @BranchId, @Salary, @HiredAt)
END
