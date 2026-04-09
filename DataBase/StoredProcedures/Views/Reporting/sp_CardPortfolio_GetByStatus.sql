CREATE PROCEDURE [dbo].[sp_CardPortfolio_GetByStatus]
    @Status NVARCHAR(50)
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
    WHERE [CardStatus] = @Status
    ORDER BY [CardholderName], [ExpirationDate];
END
