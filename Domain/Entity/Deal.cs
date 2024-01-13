using System.ComponentModel.DataAnnotations.Schema;
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
        public DateOnly ApproximateDate { get; set; }
        public string Localtion { get; set; }
        public StatusDeal Status { get; set; }
        public List<Category> Categories { get; set; }
        public int CreatorUserId { get; set; }
        public User CreatorUser { get; set; }
        public int? ExecutorUserId { get; set; }
        public User? ExecutorUser { get; set; }
        public List<Proposal> Proposals { get; set; }
    }
}
