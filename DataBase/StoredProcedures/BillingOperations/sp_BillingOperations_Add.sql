CREATE PROCEDURE [dbo].[sp_BillingOperations_Add]
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
    INSERT INTO [dbo].[BillingOperations] ([Id], [Amount], [Currency], [CreatedAt], [Description], [PaymentPurpose], [CustomerId], [BillingNumberIdFrom], [BillingNumberIdTo], [CreditId])
    VALUES (@Id, @Amount, @Currency, @CreatedAt, @Description, @PaymentPurpose, @CustomerId, @BillingNumberIdFrom, @BillingNumberIdTo, @CreditId)
END
