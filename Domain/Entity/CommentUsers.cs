using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity
{
    public class CommentUsers
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime TimeCreated { get; set; }
        public int CreatorUserId { get; set; }
        public User CreatorUser { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
