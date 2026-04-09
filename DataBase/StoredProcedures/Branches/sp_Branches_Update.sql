CREATE PROCEDURE [dbo].[sp_Branches_Update]
    @Id UNIQUEIDENTIFIER,
    @Name NVARCHAR(255),
    @NumberOfEmployees INT,
    @NumberOfBranch INT,
    @Location NVARCHAR(255),
    @ContactEmail NVARCHAR(255),
    @ContactPhone NVARCHAR(50)
AS
BEGIN
    UPDATE [dbo].[Branches]
    SET [Name] = @Name,
        [NumberOfEmployees] = @NumberOfEmployees,
        [NumberOfBranch] = @NumberOfBranch,
        [Location] = @Location,
        [ContactEmail] = @ContactEmail,
        [ContactPhone] = @ContactPhone
    WHERE [Id] = @Id
END
