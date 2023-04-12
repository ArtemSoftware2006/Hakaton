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
    public class UserController : Controller
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
                return StatusCode(400, response.Description);
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
                return StatusCode(400,response.Description);
            }
            return BadRequest("Модель не валидна");
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return Ok();
            }
            return StatusCode(403);

        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UserUpdateVM model)
        {
            if (ModelState.IsValid)
            {
                if (HttpContext.User.Identity.IsAuthenticated && HttpContext.User.IsInRole("Executor"))
                {
                    var response = await _userService.Update(model);

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
        //[Authorize("Executor")]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            if (ModelState.IsValid)
            {
                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    var response = await _userService.GetAllUsers();

                    if (response.StatusCode == Domain.Enum.StatusCode.Ok || response.StatusCode == Domain.Enum.StatusCode.NotFound)
                    {
                        return Json(response.Data);
                    }
                    return StatusCode(400, response.Description);
                }
                return StatusCode(403);
            }
            return BadRequest("Модель не валидна");
        }
        [HttpGet]
        public async Task<IActionResult> GetExecutorByCategory(int id)
        {
            if (ModelState.IsValid)
            {
                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    var response = await _userService.GetExecutorByCategory(id);

                    if (response.StatusCode == Domain.Enum.StatusCode.Ok || response.StatusCode == Domain.Enum.StatusCode.NotFound)
                    {
                        return Json(response.Data);
                    }
                    return StatusCode(400, response.Description);
                }
                return StatusCode(403);
            }
            return BadRequest("Модель не валидна");
        }
    }
}
