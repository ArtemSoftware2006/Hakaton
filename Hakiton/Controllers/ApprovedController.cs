using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Impl;
using Services.Interfaces;

namespace Hakiton.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ApprovedController : Controller
    {
        public IApprovedDealService _approvedDealService { get; set; }

        public ApprovedController(IApprovedDealService approvedDealService)
        {
            _approvedDealService = approvedDealService;
        }
        [HttpPost]
        public async Task<IActionResult> ApprovedDeal(int dealId,int UserId)
        {
            if (ModelState.IsValid)
            {
                if (HttpContext.User.Identity.IsAuthenticated && HttpContext.User.IsInRole("Employer"))
                {
                    var response = await _approvedDealService.ConfirmDeal(dealId, UserId);

                    if (response.StatusCode == Domain.Enum.StatusCode.Ok)
                    {
                        return Json(response.Data);
                    }
                    return StatusCode(400, response.Description);
                }
                return StatusCode(403);
            }
            return BadRequest("Модель не валидна");
        }
        [HttpGet]
        public async Task<IActionResult> GetDeals(int UserId)
        {
            if (ModelState.IsValid)
            {
                if (HttpContext.User.Identity.IsAuthenticated && HttpContext.User.IsInRole("Employer"))
                {
                    var response = await _approvedDealService.GetAllConfirmDeal(UserId);

                    if (response.StatusCode == Domain.Enum.StatusCode.Ok)
                    {
                        return Json(response.Data);
                    }
                    return StatusCode(400, response.Description);
                }
                return StatusCode(403);
            }
            return BadRequest("Модель не валидна");
        }
        [HttpGet]
        public async Task<IActionResult> GetProposals(int UserId)
        {
            if (ModelState.IsValid)
            {
                if (HttpContext.User.Identity.IsAuthenticated && HttpContext.User.IsInRole("Executor"))
                {
                    var response = await _approvedDealService.GetAllConfirmProposal(UserId);

                    if (response.StatusCode == Domain.Enum.StatusCode.Ok)
                    {
                        return Json(response.Data);
                    }
                    return StatusCode(400, response.Description);
                }
                return StatusCode(403);
            }
            return BadRequest("Модель не валидна");
        }
    }
}
