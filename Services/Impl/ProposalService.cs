﻿using DAL.Interfaces;
using Domain.Entity;
using Domain.Response;
using Domain.ViewModel.Proposal;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Impl
{
    public class ProposalService : IProposalService
    {
        public IProposalRepository _propposalRepository { get; set; }

        public ProposalService(IProposalRepository propposalRepository)
        {
            _propposalRepository = propposalRepository;
        }

        public async Task<BaseResponse<bool>> Create(ProposalCreateVM model)
        {
            try
            {
                var proposal = new Proposal()
                {
                    Descripton= model.Descripton,
                    Price= model.Price,
                    UserId= model.UserId,
                    DealId = model.DealId,
                    DatePublish = DateTime.UtcNow,
                };

                await _propposalRepository.Create(proposal);

                return new BaseResponse<bool>()
                {
                    Data = true,
                    Description = "Ok",
                    StatusCode = Domain.Enum.StatusCode.Ok,
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

        public Task<BaseResponse<bool>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<Proposal>> Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse<List<Proposal>>> GetAll()
        {
            try
            {
                var proposals = _propposalRepository.GetAll().ToList();
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

        public Task<BaseResponse<Proposal>> GetByTitle(string Title)
        {
            throw new NotImplementedException();
        }
    }
}
