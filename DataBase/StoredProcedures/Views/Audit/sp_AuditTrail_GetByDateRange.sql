CREATE PROCEDURE [dbo].[sp_AuditTrail_GetByDateRange]
    @StartDate DATETIME,
    @EndDate DATETIME
AS
BEGIN
    SELECT 
        [Id],
        [EventDate],
        [Operation],
        [User],
        [AffectedTable],
        [Description],
        [Month],
        [Year],
        [DayOfWeek]
    FROM [dbo].[v_AuditTrailByOperation]
    WHERE [EventDate] BETWEEN @StartDate AND @EndDate
END
