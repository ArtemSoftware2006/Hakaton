namespace Domain.Entity
{
    public class DealHasProposal
    {
        public int Id { get; set; }
        public int ProposalId{ get; set; }
        public Proposal Proposal { get; set; }
        public int DealId { get; set; }
        public Deal Deal { get; set; }
    }
}