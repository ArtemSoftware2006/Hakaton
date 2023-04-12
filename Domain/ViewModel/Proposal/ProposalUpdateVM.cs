using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModel.Proposal
{
    public class ProposalUpdateVM
    {
        public int Id { get; set; }
        public string? Descripton { get; set; }
        public int? Price { get; set; }
    }
}
