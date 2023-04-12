using Domain.Entity;
using Domain.Response;
using Domain.ViewModel.Deal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
