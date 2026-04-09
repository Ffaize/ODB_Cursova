CREATE PROCEDURE [dbo].[sp_Branches_Add]
    @Id UNIQUEIDENTIFIER,
    @Name NVARCHAR(255),
    @NumberOfEmployees INT,
    @NumberOfBranch INT,
    @Location NVARCHAR(255),
    @ContactEmail NVARCHAR(255),
    @ContactPhone NVARCHAR(50),
    @CreatedAt DATETIME2 = NULL
AS
BEGIN
    INSERT INTO [dbo].[Branches] ([Id], [Name], [NumberOfEmployees], [NumberOfBranch], [Location], [ContactEmail], [ContactPhone], [CreatedAt])
    VALUES (@Id, @Name, @NumberOfEmployees, @NumberOfBranch, @Location, @ContactEmail, @ContactPhone, ISNULL(@CreatedAt, GETUTCDATE()))
END
