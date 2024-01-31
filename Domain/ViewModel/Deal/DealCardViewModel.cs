using Domain.Entity;
using Domain.Enum;
using Domain.ViewModel.User;

namespace Domain.ViewModel.Deal
{
    public class DealCardViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Views { get; set; }
        public bool AlreadyResponded { get; set; }
        public StatusDeal Status { get; set; }
        public DateTime DatePublication { get; set; }
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
        public DateOnly ApproximateDate { get; set; }
        public string Localtion { get; set; }
        public List<Category> Categories { get; set; }
        public int CreatorUserId { get; set; }
        public UserCardViewModel CreatorUser { get; set; }
    }
}