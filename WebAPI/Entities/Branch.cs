namespace WebAPI.Entities
{
    public class Branch
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int NumberOfEmployees { get; set; }
        public int NumberOfBranch { get; set; }
        public string Location { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public DateTime CreatedAt { get; set; }
        
    }
}
