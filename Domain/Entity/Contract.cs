namespace Domain.Entity
{
    public class Contract
    {
        public int Id { get; set; }
        public int DealId { get; set; }
        public Deal Deal { get; set; }
        public int ProposalId{ get; set; }
        public Proposal Proposal { get; set; }
        public int ExecutorId { get; set; }
        public User Executor { get; set; }
        public int EmployerId { get; set; }
        public User Employer { get; set; }
    }
}