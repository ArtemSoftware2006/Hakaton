namespace Domain.ViewModel.Deal
{
    public class DealCreateViewModel
    {
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateOnly ApproximateDate { get; set; }
        public string Location { get; set; }
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
        public int[] CategoryIds { get; set; }
    }
}
