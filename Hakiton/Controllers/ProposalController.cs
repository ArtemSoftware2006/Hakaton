using Domain.ViewModel.Proposal;
using Microsoft.AspNetCore.Mvc;
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

            if (response.StatusCode == Domain.Enum.StatusCode.Ok || response.StatusCode == Domain.Enum.StatusCode.NotFound)
            {
                return Json(response.Data);
            }
            return BadRequest(response.Description);
        }
        [HttpGet]
        public async Task<IActionResult> GetByUserId(int UserId)
        {
            var response = await _proposalService.GetByUserId(UserId);

            if (response.StatusCode == Domain.Enum.StatusCode.Ok ||response.StatusCode == Domain.Enum.StatusCode.NotFound)
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
                    if (response.StatusCode == Domain.Enum.StatusCode.Ok)
                    {
                        return Ok();
                    }
                    if (response.StatusCode == Domain.Enum.StatusCode.NotFound)
                    {
                        return StatusCode(400, response.Description);
                    }
                    return StatusCode(500, response.Description);
                }
                return StatusCode(403);
            }
            return BadRequest("Ошибка");
        }
        [HttpGet]
        public async Task<IActionResult> GetByDealId(int id)
        {
            var response = await _proposalService.GetByDealId(id);

            if (response.StatusCode == Domain.Enum.StatusCode.Ok || response.StatusCode == Domain.Enum.StatusCode.NotFound)
            {
                return Json(response.Data);
            }
            return BadRequest(response.Description);
        }
        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            if (ModelState.IsValid)
            {
                if (HttpContext.User.Identity.IsAuthenticated && HttpContext.User.IsInRole("Executor"))
                {
                    var response = await _proposalService.Get(id);

                    if (response.StatusCode == Domain.Enum.StatusCode.Ok)
                    {
                        return Json(response.Data);
                    }
                    return StatusCode(400,response.Description);
                }

                return StatusCode(403);
            }
            return BadRequest("Модель не валидна");
        }
        [HttpPatch]
        public async Task<IActionResult> Update([FromBody] ProposalUpdateVM model)
        {
            if (ModelState.IsValid)
            {
                if (HttpContext.User.Identity.IsAuthenticated && HttpContext.User.IsInRole("Executor"))
                {
                    var response = await _proposalService.Update(model);

                    if (response.StatusCode == Domain.Enum.StatusCode.Ok)
                    {
                        return Ok();
                    }
                    if (response.StatusCode == Domain.Enum.StatusCode.NotFound)
                    {
                        return StatusCode(400, response.Description);
                    }
                    return StatusCode(500, response.Description);
                }
                return StatusCode(403);
            }
            return BadRequest("Ошибка");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if (ModelState.IsValid)
            {
                if (HttpContext.User.Identity.IsAuthenticated && HttpContext.User.IsInRole("Executor"))
                {
                    var response = _proposalService.Delete(id);
                    return Ok();
                }
                return StatusCode(403);
            }
            return BadRequest("Модель не валидна");
        }

    }
}
