using Domain.Enum;

namespace Domain.Entity
{
    public class Deal
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DatePublication { get; set; }
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? StopDate { get; set; }
        public string? location { get; set; }
        public StatusDeal Status { get; set; }
        public int? ApprovedDealId { get; set; }
        public ApprovedDeal? ApprovedDeal { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<Proposal> Proposals { get; set; }
    }
}
