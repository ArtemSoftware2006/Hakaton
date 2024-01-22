using System.Text.Json.Serialization;
using Domain.Enum;

namespace Domain.Entity
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
        public string? FirstName { get; set; }
        public string? SecondName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
        public bool IsVIP { get; set; }
        public int Balance { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Description { get; set; }
        public List<CommentUsers> CommentUsers { get; set; }
        public List<CommentDeals> CommentDeals { get; set; }
        [JsonIgnore]
        public List<Category> Categories { get; set; }
        [JsonIgnore]
        public List<Proposal> Proposals { get; set; }
        [JsonIgnore]
        public List<Deal> CreatedDeals { get; set; }
        public List<Deal> AcceptedDeals { get; set; }
    }
}
