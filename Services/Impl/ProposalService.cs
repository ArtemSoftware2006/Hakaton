using DAL.Interfaces;
using Domain.Entity;
using Domain.Response;
using Domain.ViewModel.Proposal;
using Microsoft.Extensions.Logging;
using Services.Interfaces;

namespace Services.Impl
{
    public class ProposalService : IProposalService
    {
        public IProposalRepository _proposalRepository { get; set; }
        public IDealRepository _dealRepository { get; set; }
        private readonly ILogger<ProposalService> _logger;

        public ProposalService(
            IProposalRepository propposalRepository,
            IDealRepository dealRepository,
            ILogger<ProposalService> logger
        )
        {
            _proposalRepository = propposalRepository;
            _dealRepository = dealRepository;
            _logger = logger;
        }

        public async Task<BaseResponse<bool>> Create(ProposalCreateViewModel model)
        {
            try
            {
                var proposal = _proposalRepository
                    .GetAll()
                    .Where(x => x.DealId == model.DealId && x.UserId == model.UserId)
                    .FirstOrDefault();
                if (proposal == null)
                {
                    //TODO Добавить маппер
                    proposal = new Proposal()
                    {
                        Descripton = model.Description,
                        Price = model.Price,
                        UserId = model.UserId,
                        DealId = model.DealId,
                        DatePublish = DateTime.UtcNow,
                        Status = Domain.Enum.StatusDeal.Published,
                    };
                    await _proposalRepository.Create(proposal);
                    return new BaseResponse<bool>()
                    {
                        Data = true,
                        Description = "Ok",
                        StatusCode = Domain.Enum.StatusCode.Ok,
                    };
                }

                return new BaseResponse<bool>()
                {
                    Data = false,
                    Description = "Вы уже отправили заявку",
                    StatusCode = Domain.Enum.StatusCode.NotFound,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Data = false,
                    Description = ex.Message,
                    StatusCode = Domain.Enum.StatusCode.InternalServiseError,
                };
            }
        }

        public async Task<BaseResponse<bool>> Delete(int id)
        {
            try
            {
                var proposal = await _proposalRepository.Get(id);
                if (proposal != null)
                {
                    _proposalRepository.Delete(proposal);
                    return new BaseResponse<bool>()
                    {
                        Data = true,
                        StatusCode = Domain.Enum.StatusCode.Ok,
                        Description = "Ok",
                    };
                }
                return new BaseResponse<bool>()
                {
                    StatusCode = Domain.Enum.StatusCode.NotFound,
                    Description = $"нет заявки с id = {id}"
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

        public async Task<BaseResponse<Proposal>> Get(int id)
        {
            try
            {
                var proposal = await _proposalRepository.Get(id);
                if (proposal != null)
                {
                    return new BaseResponse<Proposal>()
                    {
                        Data = proposal,
                        StatusCode = Domain.Enum.StatusCode.Ok,
                        Description = "Ok",
                    };
                }
                return new BaseResponse<Proposal>()
                {
                    StatusCode = Domain.Enum.StatusCode.NotFound,
                    Description = $"нет заявки с id = {id}"
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Proposal>()
                {
                    Description = ex.Message,
                    StatusCode = Domain.Enum.StatusCode.InternalServiseError,
                };
            }
        }

        public async Task<BaseResponse<List<Proposal>>> GetAll()
        {
            try
            {
                var proposals = _proposalRepository.GetAll().ToList();
                if (proposals.Count != 0)
                {
                    return new BaseResponse<List<Proposal>>()
                    {
                        Data = proposals,
                        Description = "ok",
                        StatusCode = Domain.Enum.StatusCode.Ok,
                    };
                }
                return new BaseResponse<List<Proposal>>
                {
                    Data = proposals,
                    StatusCode = Domain.Enum.StatusCode.NotFound,
                    Description = "нет заявок",
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

        public async Task<BaseResponse<List<Proposal>>> GetByDealId(int id)
        {
            try
            {
                var proposals = _proposalRepository.GetAll().Where(x => x.DealId == id).ToList();
                if (proposals.Count != 0)
                {
                    return new BaseResponse<List<Proposal>>()
                    {
                        Data = proposals,
                        Description = "Ok",
                        StatusCode = Domain.Enum.StatusCode.Ok
                    };
                }
                return new BaseResponse<List<Proposal>>
                {
                    Data = proposals,
                    Description = $"Нет Заявок выполнить запрос с id = {id}",
                    StatusCode = Domain.Enum.StatusCode.NotFound,
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

        public async Task<BaseResponse<Proposal>> Update(ProposalUpdateViewModel model)
        {
            try
            {
                var proposal = await _proposalRepository.Get(model.Id);
                if (proposal != null)
                {
                    proposal.Price = model.Price ?? proposal.Price;
                    proposal.Descripton = model.Descripton ?? model.Descripton;

                    await _proposalRepository.Update(proposal);

                    return new BaseResponse<Proposal>()
                    {
                        Data = proposal,
                        Description = "Ok",
                        StatusCode = Domain.Enum.StatusCode.Ok,
                    };
                }

                return new BaseResponse<Proposal>()
                {
                    Description = $"нет заявки с id = {model.Id}",
                    StatusCode = Domain.Enum.StatusCode.NotFound,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Proposal>()
                {
                    Description = ex.Message,
                    StatusCode = Domain.Enum.StatusCode.InternalServiseError,
                };
            }
        }

        public async Task<BaseResponse<List<Proposal>>> GetByUserId(int id)
        {
            try
            {
                var proposals = _proposalRepository.GetAll().Where(x => x.UserId == id).ToList();
                if (proposals.Count != 0)
                {
                    return new BaseResponse<List<Proposal>>()
                    {
                        Data = proposals,
                        Description = "ok",
                        StatusCode = Domain.Enum.StatusCode.Ok,
                    };
                }
                return new BaseResponse<List<Proposal>>
                {
                    Data = proposals,
                    StatusCode = Domain.Enum.StatusCode.NotFound,
                    Description = "нет заявок",
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

        public async Task<BaseResponse<List<Proposal>>> GetAllByUserDeals(int id)
        {
            try
            {
                _logger.LogInformation($"Id пользователя = {id}");
                var userDeals = _dealRepository
                    .GetAll()
                    .Where(x => x.CreatorUserId == id)
                    .Select(x => x.Id);
                _logger.LogInformation($"Количество Заказов = {userDeals.Count()}");
                var proposals = _proposalRepository
                    .GetAll()
                    .Where(x => userDeals.Contains(x.DealId))
                    .ToList();

                if (proposals.Count() != 0)
                {
                    return new BaseResponse<List<Proposal>>()
                    {
                        Data = proposals,
                        Description = "Ok",
                        StatusCode = Domain.Enum.StatusCode.Ok
                    };
                }
                return new BaseResponse<List<Proposal>>
                {
                    Data = proposals,
                    Description = $"Нет Заявок на выполнение заказа для пользователя с id = {id}",
                    StatusCode = Domain.Enum.StatusCode.NotFound,
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
    }
}
