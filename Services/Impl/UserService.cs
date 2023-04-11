using DAL.Interfaces;
using Domain.Entity;
using Domain.Enum;
using Domain.Helper;
using Domain.Response;
using Domain.ViewModel.User;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Service.Impl
{
    public class UserService : IUserService
    {
        public IUserRepository _userRepository { get; set; }

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<BaseResponse<bool>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse<User>> Get(int id)
        {
            try
            {
                var user = await _userRepository.Get(id);

                if (user != null)
                {
                    return new BaseResponse<User>()
                    {
                        Data = user,
                        Description = "Ok",
                        StatusCode = StatusCode.Ok,
                    };
                }
                return new BaseResponse<User>()
                {
                    Description = "нет пользователя с таким id",
                    StatusCode = StatusCode.NotFound,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<User>
                {
                    StatusCode = StatusCode.InternalServiseError,
                    Description = ex.Message,
                };
            }
        }

        public Task<BaseResponse<User>> GetByLogin(string login)
        {
            throw new NotImplementedException();
        }
        public async Task<BaseResponse<bool>> SetCategory(UserSetCategoryVM model)
        {
            try
            {
                var user =  await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Id == model.UserId);   
                if (user != null)
                {
                    user.CategoryId = model.CategoryId;
                    await _userRepository.Update(user);

                    return new BaseResponse<bool>()
                    {
                        Data = true,
                        Description = "Ok",
                        StatusCode = StatusCode.Ok,
                    };
                }
                return new BaseResponse<bool>()
                {
                    Data = false,
                    Description = $"Пользователя с id{model.UserId}",
                    StatusCode = StatusCode.NotFound,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>
                {
                    StatusCode = StatusCode.InternalServiseError,
                    Description = ex.Message,
                };
            }
        }
        public async Task<BaseResponse<ClaimsIdentity>> Registr(UserRegistrVM model)
        {
            try
            {
                Domain.Entity.User user = _userRepository.GetAll().FirstOrDefault(x => x.Email == model.Email 
                    || x.Login == model.Login);

                if (user == null)
                {
                    user = new Domain.Entity.User()
                    {
                        Email = model.Email,
                        Login = model.Login,
                        Password = HashPasswordHelper.HashPassword(model.Password),
                        Role = (Role)model.Role
                    };

                    await _userRepository.Create(user);
                    

                    var result = Authenticate(user);


                    return new BaseResponse<ClaimsIdentity>
                    {
                        StatusCode = StatusCode.Ok,
                        Description = "OK",
                        Data = result,
                    };
                }
                else
                {
                    return new BaseResponse<ClaimsIdentity>
                    {
                        StatusCode = StatusCode.NotFound,
                        Description = "Пользователь с таким логином или почтой существует",
                    };
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<ClaimsIdentity>
                {
                    StatusCode = StatusCode.InternalServiseError,
                    Description = ex.Message,
                };
            }
        }
        public async Task<BaseResponse<ClaimsIdentity>> Login(UserLoginVM model)
        {
            try
            {
                var user = _userRepository.GetAll().FirstOrDefault(x => x.Login == model.Login && x.Password == HashPasswordHelper.HashPassword(model.Password));

                if (user != null)
                {
                    var result = Authenticate(user);

                    return new BaseResponse<ClaimsIdentity>
                    {
                        StatusCode = StatusCode.Ok,
                        Description = "OK",
                        Data = result,
                    };
                }
                return new BaseResponse<ClaimsIdentity>
                {
                    StatusCode = StatusCode.NotFound,
                    Description = "Неверный логин или пароль.",
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ClaimsIdentity>
                {
                    StatusCode = StatusCode.InternalServiseError,
                    Description = $"[Login(User)] : {ex.Message})",
                };
            }
        }
        public async Task<BaseResponse<List<User>>> GetExecutorByCategory(int id)
        {
            try
            {
                var executors = _userRepository.GetAll().Where(x => x.CategoryId == id && x.Role == Role.Executor).ToList();

                return new BaseResponse<List<User>>()
                {
                    Data = executors,
                    Description = "Ok",
                    StatusCode = StatusCode.Ok,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<User>>
                {
                    StatusCode = StatusCode.InternalServiseError,
                    Description = $"[Login(User)] : {ex.Message})",
                };
            }
        }
        public async Task<BaseResponse<List<User>>> GetAllUsers()
        {
            try
            {
                var users = _userRepository.GetAll().ToList();
                return new BaseResponse<List<User>>()
                {
                    Data = users,
                    Description = "Ok",
                    StatusCode = StatusCode.Ok,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<User>>
                {
                    StatusCode = StatusCode.InternalServiseError,
                    Description = $"[Login(User)] : {ex.Message})",
                };
            }
        }

        private ClaimsIdentity Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            return new ClaimsIdentity(claims, "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }

    }
}
