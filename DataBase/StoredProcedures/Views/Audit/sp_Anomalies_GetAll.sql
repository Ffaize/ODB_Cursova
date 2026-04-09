CREATE PROCEDURE [dbo].[sp_Anomalies_GetAll]
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
END
