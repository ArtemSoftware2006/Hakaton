namespace Domain.Entity
{
    public class Avatar
    {
        public int Id { get; set; }
        public Guid Key { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } 
    }
}