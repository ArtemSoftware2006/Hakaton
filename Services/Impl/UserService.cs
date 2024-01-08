using DAL.Interfaces;
using Domain.Entity;
using Domain.Enum;
using Domain.Helper;
using Domain.Response;
using Domain.ViewModel.User;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;

namespace Service.Impl
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMoneyRepository _moneyRepository;

        public UserService(IUserRepository userRepository, IMoneyRepository moneyRepository)
        {
            _moneyRepository = moneyRepository;
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
                var user = await _userRepository
                    .GetAll()
                    .FirstOrDefaultAsync(x => x.Id == model.UserId);
                if (user != null)
                {
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

        public async Task<BaseResponse<User>> Registr(UserRegistrVM model)
        {
            try
            {
                User user = _userRepository
                    .GetAll()
                    .FirstOrDefault(x => x.Email == model.Email || x.Login == model.Login);

                if (user == null)
                {
                    user = new User()
                    {
                        Email = model.Email,
                        Login = model.Login,
                        Password = HashPasswordHelper.HashPassword(model.Password),
                        Role = Role.User,
                        Balance = 1000,
                        IsVIP = false,
                    };

                    await _userRepository.Create(user);

                    return new BaseResponse<User>
                    {
                        StatusCode = StatusCode.Ok,
                        Description = "OK",
                        Data = user,
                    };
                }
                else
                {
                    return new BaseResponse<User>
                    {
                        StatusCode = StatusCode.NotFound,
                        Description = "Пользователь с таким логином или почтой существует",
                    };
                }
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

        public async Task<BaseResponse<User>> Login(UserLoginVM model)
        {
            try
            {
                var user = _userRepository
                    .GetAll()
                    .FirstOrDefault(
                        x =>
                            x.Login == model.Login
                            && x.Password == HashPasswordHelper.HashPassword(model.Password)
                    );

                if (user != null)
                {
                    return new BaseResponse<User>
                    {
                        StatusCode = StatusCode.Ok,
                        Description = "OK",
                        Data = user,
                    };
                }
                return new BaseResponse<User>
                {
                    StatusCode = StatusCode.NotFound,
                    Description = "Неверный логин или пароль.",
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<User>
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
                    Description = ex.Message,
                };
            }
        }

        public async Task<BaseResponse<bool>> VIP(int id)
        {
            try
            {
                var user = await _userRepository.Get(id);

                if (user != null)
                {
                    if (user.Balance <= 500 || user.IsVIP == true)
                    {
                        return new BaseResponse<bool>()
                        {
                            Data = false,
                            StatusCode = StatusCode.NotFound,
                            Description = $"не хватает денег на балансе или уже есть VIP статус.."
                        };
                    }

                    if (await _moneyRepository.WithdrawMoney(user.Id, 500))
                    {
                        user.IsVIP = true;
                        await _userRepository.Update(user);
                        return new BaseResponse<bool>()
                        {
                            Data = true,
                            StatusCode = StatusCode.Ok,
                            Description = "Ok",
                        };
                    }
                    return new BaseResponse<bool>()
                    {
                        Data = false,
                        StatusCode = StatusCode.NotFound,
                        Description = "Ошибка транзакции средств с счёта пользователя.",
                    };
                }
                return new BaseResponse<bool>()
                {
                    Data = false,
                    StatusCode = StatusCode.NotFound,
                    Description = $"нет пользователя с id = {id}",
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    StatusCode = StatusCode.InternalServiseError,
                    Description = ex.Message,
                };
            }
        }

        public async Task<BaseResponse<bool>> Update(UserUpdateVM model)
        {
            try
            {
                var user = await _userRepository.Get(model.Id);
                if (user != null)
                {
                    user.FirstName = model.FirstName ?? user.FirstName;
                    user.LastName = model.LastName ?? user.LastName;
                    user.SecondName = model.SecondName ?? user.SecondName;
                    user.PhoneNumber = model.PhoneNumber ?? user.PhoneNumber;
                    user.Description = model.Description ?? user.Description;

                    _userRepository.Update(user);

                    return new BaseResponse<bool>()
                    {
                        Data = true,
                        Description = "Ok",
                        StatusCode = StatusCode.Ok,
                    };
                }
                return new BaseResponse<bool>()
                {
                    Data = true,
                    Description = $"нет пользователя с id = {model.Id}",
                    StatusCode = StatusCode.NotFound,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>
                {
                    StatusCode = StatusCode.InternalServiseError,
                    Description = $"[Login(User)] : {ex.Message})",
                };
            }
        }
    }
}
