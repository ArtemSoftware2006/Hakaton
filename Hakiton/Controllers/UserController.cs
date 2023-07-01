using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using System.Security.Claims;
using Domain.ViewModel.User;
using Hakiton;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

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
                    List<Claim> claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, response.Data.Login),
                        new Claim(ClaimTypes.Role, response.Data.Role.ToString())
                    };


                    var jwt = new JwtSecurityToken
                    (
                        claims: claims,
                        signingCredentials: new SigningCredentials(AuthTokenOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
                    );

                    jwt.Payload["email"] = response.Data.Email;
                    jwt.Payload["username"] = response.Data.Login;
                    jwt.Payload["role"] = response.Data.Role.ToString();
                    jwt.Payload["id"] = response.Data.Id;

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
                    List<Claim> claims = new List<Claim>()
                    {
                         new Claim(ClaimTypes.Name, response.Data.Login),
                        new Claim(ClaimTypes.Role, response.Data.ToString())
                    };


                    var jwt = new JwtSecurityToken
                    (
                        claims: claims,
                        signingCredentials: new SigningCredentials(AuthTokenOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
                    );

                    jwt.Payload["email"] = response.Data.Email;
                    jwt.Payload["username"] = response.Data.Login;
                    jwt.Payload["role"] = response.Data.Role.ToString();
                    jwt.Payload["id"] = response.Data.Id;

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
                HttpContext.Response.Cookies.Delete("Authorization");
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
                        return StatusCode(200,response.Data);
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
