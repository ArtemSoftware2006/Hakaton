using Domain.ViewModel.Proposal;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id) {
            var response = await _proposalService.Get(id);

            if (response.StatusCode == Domain.Enum.StatusCode.Ok)
            {
                return Ok(response.Data);
            }
            else if (response.StatusCode == Domain.Enum.StatusCode.NotFound){
                return BadRequest(response.Description);   
            }
            else {
                return StatusCode(500, response.Description);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPoroposals()
        {
            var response = await _proposalService.GetAll();

            if (
                response.StatusCode == Domain.Enum.StatusCode.Ok
                || response.StatusCode == Domain.Enum.StatusCode.NotFound
            )
            {
                return StatusCode(200, response.Data);
            }
            return BadRequest(response.Description);
        }

        [HttpGet]
        public async Task<IActionResult> GetByUserId(int UserId)
        {
            var response = await _proposalService.GetByUserId(UserId);

            if (
                response.StatusCode == Domain.Enum.StatusCode.Ok
                || response.StatusCode == Domain.Enum.StatusCode.NotFound
            )
            {
                return Json(response.Data);
            }
            return BadRequest(response.Description);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] ProposalCreateViewModel model)
        {
            if (ModelState.IsValid)
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
            return BadRequest("Ошибка");
        }

        [HttpGet]
        public async Task<IActionResult> GetByDealId(int id)
        {
            var response = await _proposalService.GetByDealId(id);

            if (
                response.StatusCode == Domain.Enum.StatusCode.Ok
                || response.StatusCode == Domain.Enum.StatusCode.NotFound
            )
            {

                return Ok(response.Data);
            }
            return BadRequest(response.Description);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllByUserDeals(int id)
        {
            var response = await _proposalService.GetAllByUserDeals(id);

            if (
                response.StatusCode == Domain.Enum.StatusCode.Ok
                || response.StatusCode == Domain.Enum.StatusCode.NotFound
            )
            {
                return Json(response.Data);
            }
            return BadRequest(response.Description);
        }

        [HttpPatch]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] ProposalUpdateViewModel model)
        {
            if (ModelState.IsValid)
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
            return BadRequest("Ошибка");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if (ModelState.IsValid)
            {
                var response = await _proposalService.Delete(id);
                return Ok(response.Data);
            }
            return BadRequest("Модель не валидна");
        }
    }
}
