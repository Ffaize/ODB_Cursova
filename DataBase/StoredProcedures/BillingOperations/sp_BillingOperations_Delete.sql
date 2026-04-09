CREATE PROCEDURE [dbo].[sp_BillingOperations_Delete]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    DELETE FROM [dbo].[BillingOperations]
    WHERE [Id] = @Id
END
