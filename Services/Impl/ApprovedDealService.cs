using DAL.Interfaces;
using Domain.Entity;
using Domain.Response;
using Domain.ViewModel.Deal;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Impl
{
    public class ApprovedDealService : IApprovedDealService
    {
        public IDealRepository _dealRepository { get; set; }
        public IProposalRepository _proposalRepository { get; set; }
        public ApprovedDealService(IDealRepository dealRepository, IProposalRepository proposalRepository)
        {
            _dealRepository = dealRepository;
            _proposalRepository = proposalRepository;
        }
        public async Task<BaseResponse<bool>> ConfirmDeal(int DealId,int ProposalId)
        {
            try
            {
                var deal = await _dealRepository.Get(DealId);

                if (deal != null)
                {
                    var proposal = await _proposalRepository.Get(ProposalId);

                    if (proposal != null)
                    {
                        deal.Status = Domain.Enum.StatusDeal.InProcess;
                        proposal.Status = Domain.Enum.StatusDeal.InProcess;

                        _dealRepository.Update(deal);
                        _proposalRepository.Update(proposal);

                        return new BaseResponse<bool>()
                        {
                            Data = true,
                            Description = "Ok",
                            StatusCode = Domain.Enum.StatusCode.Ok,
                        };
                    }
                    return new BaseResponse<bool>()
                    {
                        Description = $"нет предложения с id = {ProposalId}",
                        StatusCode = Domain.Enum.StatusCode.NotFound
                    };
                }
                return new BaseResponse<bool>()
                {
                    Description = $"нет заказа с id = {DealId}",
                    StatusCode = Domain.Enum.StatusCode.NotFound
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = ex.Message,
                    StatusCode = Domain.Enum.StatusCode.InternalServiseError,
                };
            }
        }
        public async Task<BaseResponse<List<Deal>>> GetAllConfirmDeal(int EmployerId)
        {
            try
            {
                var deals = _dealRepository.GetAll().Where(x => x.UserId == EmployerId
                    && x.Status == Domain.Enum.StatusDeal.InProcess).ToList();

                if (deals.Count != 0)
                {
                    return new BaseResponse<List<Deal>>()
                    {
                        Data = deals,
                        StatusCode = Domain.Enum.StatusCode.Ok,
                        Description = "Ok",
                    };
                }
                return new BaseResponse<List<Deal>>()
                {
                    StatusCode = Domain.Enum.StatusCode.NotFound,
                    Description = "нет утверждённых заказов",
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<Deal>>()
                {
                    Description = ex.Message,
                    StatusCode = Domain.Enum.StatusCode.InternalServiseError,
                };
            }
        }
        public async Task<BaseResponse<List<Proposal>>> GetAllConfirmProposal(int executorId)
        {
            try
            {
                var proposals = _proposalRepository.GetAll().Where(x => x.UserId == executorId
                    && x.Status == Domain.Enum.StatusDeal.InProcess).ToList();

                if (proposals.Count != 0)
                {
                    return new BaseResponse<List<Proposal>>()
                    {
                        Data = proposals,
                        StatusCode = Domain.Enum.StatusCode.Ok,
                        Description = "Ok",
                    };
                }
                return new BaseResponse<List<Proposal>>()
                {
                    StatusCode = Domain.Enum.StatusCode.NotFound,
                    Description = "нет утверждённых предложений на заказ",
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<Proposal>>()
                {
                    Description = ex.Message,
                    StatusCode = Domain.Enum.StatusCode.InternalServiseError,
                };
            }
        }
        public Task<BaseResponse<bool>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<List<Deal>>> GetByCetegory(int id)
        {
            throw new NotImplementedException();
        }
    }
}
