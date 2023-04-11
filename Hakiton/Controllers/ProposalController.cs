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
        public async Task<IActionResult> GetAllPoroposals()
        {
            var response = await _proposalService.GetAll();

            if (response.StatusCode == Domain.Enum.StatusCode.Ok)
            {
                return Json(response.Data);
            }
            return BadRequest(response.Description);
        }
        //[Authorize("Executor")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProposalCreateVM model)
        {
            if (ModelState.IsValid)
            {
                if (HttpContext.User.Identity.IsAuthenticated && HttpContext.User.IsInRole("Executor"))
                {
                    var response = await _proposalService.Create(model);
                    if (response.StatusCode == Domain.Enum.StatusCode.Ok || response.StatusCode == Domain.Enum.StatusCode.NotFound)
                    {
                        return Ok();
                    }
                    StatusCode(500, response.Description);
                }
                StatusCode(403);
            }
            return BadRequest("Ошибка");
        }
        [HttpGet]
        public async Task<IActionResult> GetByDealId(int id)
        {
            var response = await _proposalService.GetByDealId(id);

            if (response.StatusCode == Domain.Enum.StatusCode.Ok)
            {
                return Json(response.Data);
            }
            return BadRequest(response.Description);
        }
    }
}
