using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModel
{
    public class UserRegistrVM
    {
        [DataType(DataType.EmailAddress)]
        [MaxLength(100, ErrorMessage ="Длина должна быть менее 100 символов!")]
        [Required(ErrorMessage = "Укажите адрес электронной почты")]
        public string Email { get; set; }
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Укажите логин")]
        [MaxLength(50,ErrorMessage ="Длина должна быть меньше 50 символов!")]
        public string Login { get; set; }
        [DataType(DataType.Password)]
        [MaxLength(100, ErrorMessage = "Пароль должен содержать менее 100 символов")]
        [Required(ErrorMessage = "Укажите пароль")]
        [MinLength(8, ErrorMessage = "Пароль должен содержать не менее 8 символов")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Укажите пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfimPassword { get; set; }
        [DataType(DataType.Text)]
        [Required(ErrorMessage ="Вы исполнитель или заказчик?")]
        public int Role { get; set; }
    }
}
