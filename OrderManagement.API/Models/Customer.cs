namespace OrderManagement.API.Models
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string Segment { get; set; } = "Regular";
        public int OrderCount { get; set; }
    }
}
