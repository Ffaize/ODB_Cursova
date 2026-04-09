namespace WebAPI.Entities
{
    public class ActionLog
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Operation { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
    }
}
