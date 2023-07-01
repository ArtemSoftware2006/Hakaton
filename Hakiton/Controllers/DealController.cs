using Domain.ViewModel.Deal;
using Domain.ViewModel.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Service.Interfaces;
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
        public async Task<IActionResult> GetDeals()
        {
            var response = await _dealService.GetAll();

            if (response.StatusCode == Domain.Enum.StatusCode.Ok || response.StatusCode == Domain.Enum.StatusCode.NotFound)
            {
                return Json(response.Data);
            }
            return BadRequest(response.Description);
        }
        [HttpPatch]
        public async Task<IActionResult> Update([FromBody] DealUpdateVM model)
        {
            if (ModelState.IsValid)
            {
                if (HttpContext.User.Identity.IsAuthenticated && HttpContext.User.IsInRole("Employer"))
                {
                    var response = await _dealService.Update(model);
                    if (response.StatusCode == Domain.Enum.StatusCode.Ok)
                    {
                        return Ok();
                    }
                    return StatusCode(400, response.Description);
                }
                return StatusCode(403);
            }
            return BadRequest("Модель не валидна");
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if (ModelState.IsValid)
            {
                if (HttpContext.User.Identity.IsAuthenticated && HttpContext.User.IsInRole("Employer"))
                {
                    var response = await _dealService.Delete(id);
                    if (response.StatusCode == Domain.Enum.StatusCode.Ok || response.StatusCode == Domain.Enum.StatusCode.NotFound)
                    {
                        return Ok();
                    }
                    return StatusCode(500, response.Description);
                }
                return StatusCode(403);
            }
            return BadRequest("Ошибка");
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DealCreateVM model)
        {
            if (ModelState.IsValid)
            {
                if (HttpContext.User.Identity.IsAuthenticated /*&& HttpContext.User.IsInRole("Employer")*/)
                {
                    var response = await _dealService.Create(model);
                    if (response.StatusCode == Domain.Enum.StatusCode.Ok || response.StatusCode == Domain.Enum.StatusCode.NotFound)
                    {
                        return Ok();
                    }
                    return StatusCode(500, response.Description);
                }
                return StatusCode(403);
            }
            return BadRequest("Ошибка");
        }
        [HttpGet]
        public async Task<IActionResult> GetDealsByCategory(int id)
        {
            var response = await _dealService.GetByCetegory(id);

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
                var response = await _dealService.Get(id);
                if (response.StatusCode == Domain.Enum.StatusCode.Ok || response.StatusCode == Domain.Enum.StatusCode.NotFound)
                {
                    return Json(response.Data);
                }
                return BadRequest(response.Description);
            }
            return BadRequest("Ошибка");
        }
    }
}
