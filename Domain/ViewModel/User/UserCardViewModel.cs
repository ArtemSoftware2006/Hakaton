using Domain.Entity;

namespace Domain.ViewModel.User
{
    public class UserCardViewModel
    {
        public UserCardViewModel(int id, string login, bool isVIP, string? description, List<Category> categories)
        {
            Id = id;
            Login = login;
            IsVIP = isVIP;
            Description = description;
            Categories = categories;
        }

        public int Id { get; set; }
        public string Login { get; set; }
        public bool IsVIP { get; set; }
        public string? Description { get; set; }
        public List<Category> Categories { get; set; }
    }
}