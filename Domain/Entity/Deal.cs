using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class Deal
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DatePublication { get; set; }
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int EmployerId { get; set; }
        public Employer Employer { get; set; }
        public ICollection<Proposal> Proposals { get; set; }
    }
}
