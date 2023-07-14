using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Hakiton.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ApprovedController : Controller
    {
        private readonly  IApprovedDealService _approvedDealService;
        private readonly ILogger<ApprovedController> _logger;

        public ApprovedController(IApprovedDealService approvedDealService, ILogger<ApprovedController> logger)
        {
            _approvedDealService = approvedDealService;
            _logger = logger;
        }
        [HttpPost]
        public async Task<IActionResult> ApprovedDeal(int dealId,int proposalId)
        {
            if (ModelState.IsValid)
            {
                _logger.LogInformation($"Подтверждение заказа ({dealId}) на предложение ({proposalId})");
                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    var response = await _approvedDealService.ConfirmDeal(dealId, proposalId);

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
                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    var response = await _approvedDealService.GetAllConfirmDeal(UserId);

                    if (response.StatusCode == Domain.Enum.StatusCode.Ok || response.StatusCode == Domain.Enum.StatusCode.NotFound)
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
                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    var response = await _approvedDealService.GetAllConfirmProposal(UserId);

                    if (response.StatusCode == Domain.Enum.StatusCode.Ok || response.StatusCode == Domain.Enum.StatusCode.NotFound)
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
