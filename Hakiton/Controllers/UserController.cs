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
        public async Task<bool> Registr([FromBody]UserRegistrVM model)
        {
            if (ModelState.IsValid)
            {
                var response = await _userService.Registr(model);

                if (response.StatusCode == Domain.Enum.StatusCode.Ok)
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(response.Data));

                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
        [HttpPost]
        public async Task<bool> Login([FromBody] UserLoginVM model)
        {
            if (ModelState.IsValid)
            {
                var response = await _userService.Login(model);

                if (response.StatusCode == Domain.Enum.StatusCode.Ok)
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(response.Data));

                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }
        [Authorize("Executor")]
        [HttpPost]
        public async Task<IActionResult> SetCategory([FromBody]UserSetCategoryVM model)
        {
            if (ModelState.IsValid)
            {
                var response = await _userService.SetCategory(model);

                if (response.StatusCode == Domain.Enum.StatusCode.Ok)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(response.Description);
                }
            }
            return BadRequest("Ошибка");
        }
    }
}
