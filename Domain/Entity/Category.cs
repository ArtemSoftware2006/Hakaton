
using System.Text.Json.Serialization;

namespace Domain.Entity
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public List<Deal> Deals { get; set; }
        [JsonIgnore]
        public List<User> Users { get; set; }
    }
}
