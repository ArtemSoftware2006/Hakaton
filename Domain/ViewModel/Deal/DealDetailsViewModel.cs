using Domain.Entity;
using Domain.Enum;

namespace Domain.ViewModel.User
{
    public class DealDetailsViewModel
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
        public UserCardViewModel CreatorUser { get; set; }
    }
}