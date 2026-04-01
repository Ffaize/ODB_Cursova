CREATE PROCEDURE [dbo].[sp_Customers_Delete]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    DELETE FROM [dbo].[Customers]
    WHERE [Id] = @Id
END
