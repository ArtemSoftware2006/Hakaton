namespace Domain.ViewModel.Deal
{
    public class DealUpdateViewModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
        public string? Location { get; set; }
        public DateOnly? ApproximateDate { get; set; }
        public List<int>? CategoryIds { get; set; }
    }
}
