using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModel.Proposal
{
    public class ProposalCreateVM
    {
        public string Descripton { get; set; }
        public int Price { get; set; }
        public int ExecutorId { get; set; }
        public int DealId { get; set; }
    }
}
