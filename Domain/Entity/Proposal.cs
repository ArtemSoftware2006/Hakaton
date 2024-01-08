using Domain.Enum;

namespace Domain.Entity
{
    public class Proposal
    {
        public int Id { get; set; }
        public string Descripton { get; set; }
        public int Price { get; set; }
        public DateTime DatePublish { get; set; }
        public StatusDeal Status { get; set; }
        public int DealId { get; set; }
        public Deal Deal { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
