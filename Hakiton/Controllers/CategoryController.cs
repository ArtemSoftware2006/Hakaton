using Domain.Entity;
using Domain.ViewModel.User;
using Microsoft.AspNetCore.Http;
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

                if (response.StatusCode == Domain.Enum.StatusCode.Ok)
                {
                    return Json(response.Data.ToList());
                }
                return BadRequest(response.Description);
            }
            return BadRequest("ModelState is not valid");
        }

    }
}
