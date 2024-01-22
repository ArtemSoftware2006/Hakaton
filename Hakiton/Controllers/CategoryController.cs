using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Hakiton.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class CategoryController : Controller
    {
        public ICategoryService _categoryService { get; set; }

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            if (ModelState.IsValid)
            {
                var response = await _categoryService.GetAll();

                if (
                    response.StatusCode == Domain.Enum.StatusCode.Ok
                    || response.StatusCode == Domain.Enum.StatusCode.NotFound
                )
                {
                    return Json(response.Data.ToList());
                }
                return BadRequest(response.Description);
            }
            return BadRequest("ModelState is not valid");
        }
    }
}
