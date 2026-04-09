CREATE PROCEDURE [dbo].[sp_AuditTrail_GetAll]
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
END
