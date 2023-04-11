using Domain.ViewModel.Deal;
using Domain.ViewModel.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

            if (response.StatusCode == Domain.Enum.StatusCode.Ok)
            {
                return Json(response.Data);
            }
            return BadRequest(response.Description);
        }
        [Authorize("Employer")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DealCreateVM model)
        {
            if (ModelState.IsValid)
            {
                var response = await _dealService.Create(model);
                if (response.StatusCode == Domain.Enum.StatusCode.Ok)
                {
                    return Ok();
                }
                return BadRequest(response.Description);
            }
            return BadRequest("Ошибка");
        }
        [HttpGet]
        public async Task<IActionResult> GetDealsByCategory(int id)
        {
            var response = await _dealService.GetByCetegory(id);

            if (response.StatusCode == Domain.Enum.StatusCode.Ok)
            {
                return Json(response.Data);
            }
            return BadRequest(response.Description);
        }
    }
}
