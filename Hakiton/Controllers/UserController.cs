using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Domain.ViewModel.User;
using Microsoft.Extensions.Configuration.UserSecrets;
using Hakiton;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Principal;

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
                    var now = DateTime.UtcNow;

                    var jwt = new JwtSecurityToken
                    (
                        issuer: AuthTokenOptions.ISSUER,
                        audience: AuthTokenOptions.AUDIENCE,
                        notBefore: now,
                        claims: response.Data.Claims,
                        expires: now.Add(TimeSpan.FromMinutes(AuthTokenOptions.LIFETIME)),
                        signingCredentials: new SigningCredentials(AuthTokenOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
                    );

                    var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);


                    HttpContext.Response.Cookies.Append("Authorization", encodedJwt);
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
                    var now = DateTime.UtcNow;

                    var jwt = new JwtSecurityToken
                    (
                        issuer: AuthTokenOptions.ISSUER,
                        audience: AuthTokenOptions.AUDIENCE,
                        notBefore: now,
                        claims: response.Data.Claims,
                        expires: now.Add(TimeSpan.FromMinutes(AuthTokenOptions.LIFETIME)),
                        signingCredentials: new SigningCredentials(AuthTokenOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
                    );

                    var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                    HttpContext.Response.Cookies.Append("Authorization", encodedJwt);

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
                await HttpContext.Response.Cookies.Delete("Authorization");
                return Ok();
            }
            return StatusCode(403);

        }
        [HttpPatch]
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
        [HttpPut]
        public async Task<IActionResult> VIP(int id)
        {
            if (ModelState.IsValid)
            {
                if (HttpContext.User.Identity.IsAuthenticated && HttpContext.User.IsInRole("Executor"))
                {
                    var response = await _userService.VIP(id);

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
        public async Task<IActionResult> Get(int id)
        {
            if (ModelState.IsValid)
            {
                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    var response = await _userService.Get(id);

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
