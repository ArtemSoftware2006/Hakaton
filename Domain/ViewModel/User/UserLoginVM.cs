using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModel.User
{
    public class UserLoginVM
    {
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Укажите логин")]
        [MaxLength(50, ErrorMessage = "Длина должна быть меньше 50 символов!")]
        public string Login { get; set; }
        [DataType(DataType.Password)]
        [MaxLength(100, ErrorMessage = "Пароль должен содержать менее 100 символов")]
        [Required(ErrorMessage = "Укажите пароль")]
        public string Password { get; set; }
    }
}
