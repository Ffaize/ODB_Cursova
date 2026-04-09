CREATE PROCEDURE [dbo].[sp_Anomalies_BySeverity]
    @Severity NVARCHAR(50)
AS
BEGIN
    SELECT 
        [Id],
        [CustomerName],
        [Amount],
        [Currency],
        [CreatedAt],
        [AnomalyType],
        [Description],
        [RemainingBalance]
    FROM [dbo].[v_RecentAnomalies]
    WHERE [AnomalyType] = @Severity
END
