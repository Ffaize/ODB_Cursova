CREATE PROCEDURE [dbo].[sp_BillingNumbers_GetBalance]
    @BillingNumberId UNIQUEIDENTIFIER
AS
BEGIN
    SELECT
        [Balance],
        [Currency],
        [AccountNumber],
        [Status]
    FROM [dbo].[BillingNumbers]
    WHERE [Id] = @BillingNumberId;
END
