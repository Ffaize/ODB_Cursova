namespace WebAPI.Entities.ViewResults
{
    public class AuditTrailResult
    {
        public Guid Id { get; set; }
        public DateTime EventDate { get; set; }
        public string Operation { get; set; }
        public string User { get; set; }
        public string AffectedTable { get; set; }
        public string Description { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int DayOfWeek { get; set; }
    }
}
