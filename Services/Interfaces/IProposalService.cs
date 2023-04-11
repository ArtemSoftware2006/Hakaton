using Domain.Entity;
using Domain.Response;
using Domain.ViewModel.Proposal;

namespace Services.Interfaces
{
    public interface IProposalService
    {
        Task<BaseResponse<Proposal>> Get(int id);
        Task<BaseResponse<Proposal>> GetByTitle(string Title);
        Task<BaseResponse<bool>> Delete(int id);
        Task<BaseResponse<bool>> Create(ProposalCreateVM model);
        Task<BaseResponse<List<Proposal>>> GetAll();
    }
}
