namespace Domain.Entity
{
    public class Comments
    {
        public int Id { get; set; }
        public String Text { get; set; }
        public DateTime TimeCreated { get; set; }
        public int UserId { get; set; }
        public int DealId { get; set; }
        public User User { get; set; }
        public Deal Deal { get; set; }
    }
}
