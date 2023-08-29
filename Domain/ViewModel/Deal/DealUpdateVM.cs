using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModel.Deal
{
    public class DealUpdateVM
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
        public string? location { get; set; }
        public string? StartDate { get; set; }
        public string? StopDate { get; set; }
        public int? CategoryId { get; set; }
    }
}
