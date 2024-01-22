namespace Domain.ViewModel.User
{
    public class UserUpdateViewModel
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? SecondName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Description { get; set; }
        public int[]? CategoryIds { get; set; }
    }
}
