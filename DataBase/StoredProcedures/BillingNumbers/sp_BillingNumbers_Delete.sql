CREATE PROCEDURE [dbo].[sp_BillingNumbers_Delete]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    DELETE FROM [dbo].[BillingNumbers]
    WHERE [Id] = @Id
END
