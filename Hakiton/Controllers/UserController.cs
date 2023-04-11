using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Domain.ViewModel.User;

namespace Hakaton.Controllers
{

    [Route("[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IUserService _userService { get; set; }
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        public async Task<IActionResult> Registr([FromBody]UserRegistrVM model)
        {
            if (ModelState.IsValid)
            {
                var response = await _userService.Registr(model);

                if (response.StatusCode == Domain.Enum.StatusCode.Ok)
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(response.Data));

                    return Ok();
                }
                return NotFound();
            }
            return BadRequest("Модель не валидна");
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserLoginVM model)
        {
            if (ModelState.IsValid)
            {
                var response = await _userService.Login(model);

                if (response.StatusCode == Domain.Enum.StatusCode.Ok)
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(response.Data));

                    return Ok();
                }
                return NotFound(); 
            }
            return BadRequest("Модель не валидна");
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }
        //[Authorize("Executor")]
        [HttpPost]
        public async Task<IActionResult> SetCategory([FromBody]UserSetCategoryVM model)
        {
            if (ModelState.IsValid)
            {
                if (HttpContext.User.Identity.IsAuthenticated && HttpContext.User.IsInRole("Executor"))
                {
                    var response = await _userService.SetCategory(model);

                    if (response.StatusCode == Domain.Enum.StatusCode.Ok)
                    {
                        return Ok();
                    }
                    return StatusCode(500, response.Description);
                }
                return StatusCode(403);
            }
            return BadRequest("Модель не валидна");
        }
    }
}
