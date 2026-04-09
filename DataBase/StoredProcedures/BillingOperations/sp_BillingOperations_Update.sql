CREATE PROCEDURE [dbo].[sp_BillingOperations_Update]
    @Id UNIQUEIDENTIFIER,
    @Amount DECIMAL(18, 2),
    @Currency NVARCHAR(3),
    @CreatedAt DATETIME2,
    @Description NVARCHAR(255) = NULL,
    @PaymentPurpose INT,
    @CustomerId UNIQUEIDENTIFIER = NULL,
    @BillingNumberIdFrom UNIQUEIDENTIFIER = NULL,
    @BillingNumberIdTo UNIQUEIDENTIFIER = NULL,
    @CreditId UNIQUEIDENTIFIER = NULL
AS
BEGIN
    UPDATE [dbo].[BillingOperations]
    SET [Amount] = @Amount,
        [Currency] = @Currency,
        [CreatedAt] = @CreatedAt,
        [Description] = @Description,
        [PaymentPurpose] = @PaymentPurpose,
        [CustomerId] = @CustomerId,
        [BillingNumberIdFrom] = @BillingNumberIdFrom,
        [BillingNumberIdTo] = @BillingNumberIdTo,
        [CreditId] = @CreditId
    WHERE [Id] = @Id
END
