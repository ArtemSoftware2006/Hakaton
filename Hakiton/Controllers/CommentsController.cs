using Domain.ViewModel.Comments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Hakiton.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentsController : Controller
    {
        private readonly ICommentsService _commentService;

        public CommentsController(ICommentsService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int limit, int page, int dealId)
        {
            var response = await _commentService.GetAll(dealId);
            if (response.StatusCode == Domain.Enum.StatusCode.Ok)
            {
                Response.Headers.Append("x-total-count", response.Data.Count.ToString());

                return Json(response.Data.Take(limit * page));
            }
            return StatusCode(int.Parse(response.StatusCode.ToString()), response.Description);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _commentService.Get(id);
            if (response.StatusCode == Domain.Enum.StatusCode.Ok)
            {
                return Json(response.Data);
            }
            return BadRequest(response.Description);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CommentsViewModel model)
        {
            var response = await _commentService.Create(model);
            if (response.StatusCode == Domain.Enum.StatusCode.Ok)
            {
                return Ok();
            }
            return NotFound(response.Description);
        }
    }
}
