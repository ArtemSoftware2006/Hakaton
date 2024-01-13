using System.ComponentModel.DataAnnotations;
using Domain.Validators;

namespace Domain.ViewModel.User
{
    [PasswordsMatch]
    public class UserRegistrViewModel
    {
        [Required(AllowEmptyStrings = false)]
        public string Email { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Login { get; set; }
        [Required(AllowEmptyStrings = false)]
        [MinLength(6)]
        public string Password { get; set; }
        [Required(AllowEmptyStrings = false)]
        [MinLength(6)]
        public string PasswordConfirm { get; set; }
    }
}
