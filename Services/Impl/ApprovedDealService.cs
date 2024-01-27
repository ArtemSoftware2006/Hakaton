using DAL.Interfaces;
using Domain.Entity;
using Domain.Enum;
using Domain.Response;
using Services.Interfaces;

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

                    if (proposal != null &&  deal.Status ==  StatusDeal.Published)
                    {
                        deal.Status =  StatusDeal.InProcess;
                        proposal.Status =  StatusDeal.InProcess;

                        await _dealRepository.Update(deal);
                        await _proposalRepository.Update(proposal);

                        return new BaseResponse<bool>()
                        {
                            Data = true,
                            Description = "Ok",
                            StatusCode =  StatusCode.Ok,
                        };
                    }                    
                    return new BaseResponse<bool>()
                    {
                        Description = $"нет предложения с id = {ProposalId} или заказ уже выполянется другим заказчиком.",
                        StatusCode =  StatusCode.NotFound
                    };
                }
                return new BaseResponse<bool>()
                {
                    Description = $"нет заказа с id = {DealId}",
                    StatusCode =  StatusCode.NotFound
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = ex.Message,
                    StatusCode =  StatusCode.InternalServiseError,
                };
            }
        }
        public async Task<BaseResponse<List<Deal>>> GetAllConfirmDeal(int EmployerId)
        {
            try
            {
                var deals = _dealRepository
                    .GetAll()
                    .Where(x => x.CreatorUserId == EmployerId
                        && x.Status ==  StatusDeal.InProcess)
                    .ToList();

                if (deals.Count != 0)
                {
                    return new BaseResponse<List<Deal>>()
                    {
                        Data = deals,
                        StatusCode =  StatusCode.Ok,
                        Description = "Ok",
                    };
                }
                return new BaseResponse<List<Deal>>()
                {
                    StatusCode =  StatusCode.NotFound,
                    Description = "нет утверждённых заказов",
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<Deal>>()
                {
                    Description = ex.Message,
                    StatusCode =  StatusCode.InternalServiseError,
                };
            }
        }
        public async Task<BaseResponse<List<Proposal>>> GetAllConfirmProposal(int executorId)
        {
            try
            {
                var proposals = _proposalRepository
                    .GetAll()
                    .Where(x => x.UserId == executorId
                        && x.Status == StatusDeal.InProcess)
                    .ToList();

                if (proposals.Count != 0)
                {
                    return new BaseResponse<List<Proposal>>()
                    {
                        Data = proposals,
                        StatusCode =  StatusCode.Ok,
                        Description = "Ok",
                    };
                }
                return new BaseResponse<List<Proposal>>()
                {
                    StatusCode =  StatusCode.NotFound,
                    Description = "нет утверждённых предложений на заказ",
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<Proposal>>()
                {
                    Description = ex.Message,
                    StatusCode =  StatusCode.InternalServiseError,
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
