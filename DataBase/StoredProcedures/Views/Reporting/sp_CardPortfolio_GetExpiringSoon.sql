CREATE PROCEDURE [dbo].[sp_CardPortfolio_GetExpiringSoon]
    @DaysUntilExpiry INT = 90
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        [Id],
        [CardholderName],
        [CardNumberLast4],
        [CardNumber],
        [CardStatus],
        [LaunchDate],
        [ExpirationDate],
        [DaysUntilExpiration],
        [ExpirationStatus],
        [cvv],
        [AccountNumber],
        [Balance],
        [Currency]
    FROM [dbo].[v_CardPortfolioStatus]
    WHERE [DaysUntilExpiration] > 0 
        AND [DaysUntilExpiration] <= @DaysUntilExpiry
        AND [CardStatus] = 'Active'
    ORDER BY [DaysUntilExpiration] ASC, [CardholderName];
END
