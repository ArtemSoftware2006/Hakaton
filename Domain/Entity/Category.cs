
namespace Domain.Entity
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Deal> Deals { get; set; }
        public List<User> Users { get; set; }
    }
}
