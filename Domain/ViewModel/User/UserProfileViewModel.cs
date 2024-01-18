using Domain.Entity;

namespace Domain.ViewModel.User
{
    public class UserProfileViewModel
    {
        public string Login { get; set; }
        public string? FirstName { get; set; }
        public string? SecondName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
        public bool IsVIP { get; set; }
        public int Balance { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Description { get; set; }
        public List<Category> Categories { get; set; }

    }
}