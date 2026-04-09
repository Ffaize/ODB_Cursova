CREATE PROCEDURE [dbo].[sp_CardPortfolio_GetAll]
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
    ORDER BY [CardholderName], [ExpirationDate];
END
