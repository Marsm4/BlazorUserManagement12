using System.ComponentModel.DataAnnotations;

namespace BlazorUserManagement.Models
{
    public class RegisterUserDto
    {
        [Required(ErrorMessage = "Email обязателен.")]
        [EmailAddress(ErrorMessage = "Неверный формат email.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Имя обязательно.")]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Пароль обязателен.")]
        [MinLength(5, ErrorMessage = "Пароль должен содержать хотя бы 5 символов.")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[!""№;#$%^&*+_\-\\|.\/<>]).{5,}$", ErrorMessage = "Пароль должен содержать хотя бы одну заглавную букву, цифру и специальный символ.")]
        public string Password { get; set; }
    }
}//2
