using Domain.Response;
using Domain.ViewModel.Deal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Hakiton.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class DealController : Controller
    {
        public IDealService _dealService { get; set; }

        public DealController(IDealService dealService)
        {
            _dealService = dealService;
        }

        [HttpGet]
        [Authorize(policy: "User")]
        public async Task<IActionResult> GetDeals()
        {
            var response = await _dealService.GetAll();

            if (
                response.StatusCode == Domain.Enum.StatusCode.Ok
                || response.StatusCode == Domain.Enum.StatusCode.NotFound
            )
            {
                return Json(response.Data);
            }
            return BadRequest(response.Description);
        }

        [HttpGet]
        public async Task<IActionResult> Deals([FromQuery] int limit, int page, int userId)
        {
            var response = await _dealService.GetAll(page, limit, userId);

            if (
                response.StatusCode == Domain.Enum.StatusCode.Ok
                || response.StatusCode == Domain.Enum.StatusCode.NotFound
            )
            {
                Response.Headers.Append("x-total-count", response.Data.Total.ToString());

                return Json(response.Data.Deals);
            }
            return BadRequest(response.Description);
        }

        [HttpPatch]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] DealUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _dealService.Update(model);
                if (response.StatusCode == Domain.Enum.StatusCode.Ok)
                {
                    return Ok();
                }
                return StatusCode(400, response.Description);
            }
            return BadRequest("Модель не валидна");
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            if (ModelState.IsValid)
            {
                var response = await _dealService.Delete(id);
                if (
                    response.StatusCode == Domain.Enum.StatusCode.Ok
                    || response.StatusCode == Domain.Enum.StatusCode.NotFound
                )
                {
                    return Ok();
                }
                return StatusCode(500, response.Description);
            }
            return BadRequest("Ошибка");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] DealCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _dealService.Create(model);
                if (
                    response.StatusCode == Domain.Enum.StatusCode.Ok
                    || response.StatusCode == Domain.Enum.StatusCode.NotFound
                )
                {
                    return Ok();
                }
                return StatusCode(500, response.Description);
            }
            return BadRequest("Ошибка");
        }
        [HttpGet]
        public async Task<IActionResult> GetByTitle(string title) {
            var response = await _dealService.GetByTitle(title);

            if (
                response.StatusCode == Domain.Enum.StatusCode.Ok
                || response.StatusCode == Domain.Enum.StatusCode.NotFound
            )
            {
                return Json(response.Data);
            }
            return BadRequest(response.Description);
        }

        [HttpGet]
        public async Task<IActionResult> GetDealsByCategory(int id)
        {
            var response = await _dealService.GetByCetegory(id);

            if (
                response.StatusCode == Domain.Enum.StatusCode.Ok
                || response.StatusCode == Domain.Enum.StatusCode.NotFound
            )
            {
                return Json(response.Data);
            }
            return BadRequest(response.Description);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> Get(int id, int userId)
        {
            if (ModelState.IsValid)
            {
                var response = await _dealService.Get(id, userId);
                if (
                    response.StatusCode == Domain.Enum.StatusCode.Ok
                    || response.StatusCode == Domain.Enum.StatusCode.NotFound
                )
                {
                    return Json(response.Data);
                }
                return BadRequest(response.Description);
            }
            return BadRequest("Ошибка");
        }

        [HttpGet]
        public async Task<IActionResult> GetByUserId(int id)
        {
            var response = await _dealService.GetByUserId(id);

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
        public async Task<IActionResult> Close(int dealId)
        {
            var resonse = await _dealService.CloseDeal(dealId);

            if (resonse.StatusCode == Domain.Enum.StatusCode.Ok)
            {
                return Ok(resonse.Data);
            }
            else if (resonse.StatusCode == Domain.Enum.StatusCode.NotFound) {
                return BadRequest(resonse.Description);
            }
            else {
                return StatusCode(500, resonse.Description);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddCategories([FromBody]DealAddCategoriesViewModel model)
        {
            if (ModelState.IsValid)
            {
                BaseResponse<bool> result = await _dealService.AddCategories(model);

                if (result.StatusCode == Domain.Enum.StatusCode.NotFound)
                {
                    return NotFound( new {result.Data, result.Description});
                }
                if (result.StatusCode == Domain.Enum.StatusCode.InternalServiseError)
                {
                    return StatusCode(500, new {result.Data, result.Description});
                }
                return Ok(result.Data);   
            }

            return BadRequest("Модель не валидна!");
        }
    }
}
