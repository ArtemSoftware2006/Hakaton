
namespace Domain.Entity
{
    public class CommentDeals
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime TimeCreated { get; set; }
        public int CreatorUserId { get; set; }
        public User CreatorUser { get; set; }
        public int DealId { get; set; }
        public Deal Deal { get; set; }
    }
}
