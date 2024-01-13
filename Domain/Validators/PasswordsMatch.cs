using System.ComponentModel.DataAnnotations;
using Domain.ViewModel.User;

namespace Domain.Validators
{
    public class PasswordsMatchAttribute : ValidationAttribute
    {
       public PasswordsMatchAttribute() {
            
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            UserRegistrViewModel model = value as UserRegistrViewModel;

            if (model is null)
            {
                return new ValidationResult("Не удалось получить модель пользователя");
            }

            if (model.Password.Trim() != model.PasswordConfirm.Trim())
            {
                var errorMessage = $"Пароли не совпадают ({validationContext.DisplayName})";

                return new ValidationResult(errorMessage, new List<string>() { nameof(model.Password) });
            }
            
            return ValidationResult.Success;
        }
    }
}