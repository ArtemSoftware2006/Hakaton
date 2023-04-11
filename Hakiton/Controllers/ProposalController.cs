using Domain.Entity;
using Domain.ViewModel.Deal;
using Domain.ViewModel.Proposal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Impl;
using Services.Interfaces;

namespace Hakiton.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ProposalController : Controller
    {
        public IProposalService _proposalService { get; set; }

        public ProposalController(IProposalService proposalService)
        {
            _proposalService = proposalService;
        }
        [HttpGet]
        public async Task<IActionResult> GetPoroposal()
        {
            var response = await _proposalService.GetAll();

            if (response.StatusCode == Domain.Enum.StatusCode.Ok)
            {
                return Json(response.Data);
            }
            return BadRequest(response.Description);
        }
        [Authorize("Executor")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProposalCreateVM model)
        {
            if (ModelState.IsValid)
            {
                var response = await _proposalService.Create(model);
                if (response.StatusCode == Domain.Enum.StatusCode.Ok)
                {
                    return Ok();
                }
                return BadRequest(response.Description);
            }
            return BadRequest("Ошибка");
        }
    }
}
