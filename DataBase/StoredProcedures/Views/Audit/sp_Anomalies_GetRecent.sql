CREATE PROCEDURE [dbo].[sp_Anomalies_GetRecent]
    @HoursBack INT = 24
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
    WHERE [CreatedAt] > DATEADD(HOUR, -@HoursBack, GETUTCDATE())
END
