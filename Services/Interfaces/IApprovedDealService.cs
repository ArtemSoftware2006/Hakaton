using Domain.Entity;
using Domain.Response;

namespace Services.Interfaces
{
    public interface IApprovedDealService
    {
        Task<BaseResponse<bool>> Delete(int id);
        Task<BaseResponse<bool>> ConfirmDeal(int DealId, int PorposalId);
        Task<BaseResponse<List<Deal>>> GetAllConfirmDeal(int EmployerI);
        Task<BaseResponse<List<Deal>>> GetByCetegory(int id);
        Task<BaseResponse<List<Proposal>>> GetAllConfirmProposal(int executoId);
    }
}
