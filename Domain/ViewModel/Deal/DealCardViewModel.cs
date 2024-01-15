using Domain.Entity;

namespace Domain.ViewModel.Deal
{
    public class DealCardViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DatePublication { get; set; }
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
        public DateOnly ApproximateDate { get; set; }
        public string Localtion { get; set; }
        public List<Category> Categories { get; set; }
        public int CreatorUserId { get; set; }
        // public User CreatorUser { get; set; }
    }
}