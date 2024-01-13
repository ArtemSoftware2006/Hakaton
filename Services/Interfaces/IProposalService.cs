using Domain.Entity;
using Domain.Response;
using Domain.ViewModel.Proposal;

namespace Services.Interfaces
{
    public interface IProposalService
    {
        Task<BaseResponse<Proposal>> Update(ProposalUpdateViewModel model);
        Task<BaseResponse<Proposal>> Get(int id);
        Task<BaseResponse<List<Proposal>>> GetByDealId(int id);
        Task<BaseResponse<List<Proposal>>> GetByUserId(int id);
        Task<BaseResponse<List<Proposal>>> GetAllByUserDeals(int id);
        Task<BaseResponse<bool>> Delete(int id);
        Task<BaseResponse<bool>> Create(ProposalCreateViewModel model);
        Task<BaseResponse<List<Proposal>>> GetAll();
    }
}
