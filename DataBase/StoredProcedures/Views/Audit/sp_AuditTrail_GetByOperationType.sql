CREATE PROCEDURE [dbo].[sp_AuditTrail_GetByOperationType]
    @OperationType NVARCHAR(50)
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
    WHERE [Operation] = @OperationType
END
