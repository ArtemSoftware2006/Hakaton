using Domain.Entity;

namespace DAL.Interfaces
{
    public interface IProposalRepository : IBaseRepository<Proposal> 
    {
        Task<Proposal> GetWithUser(int id);
    }
}
