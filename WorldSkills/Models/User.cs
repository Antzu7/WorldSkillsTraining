using System.ComponentModel.DataAnnotations;

namespace WorldSkills.Models
{
    internal class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [StringLength(100, ErrorMessage = "Пароль должен содержать как минимум 6 символов", MinimumLength = 6)]        
        public string Password { get; set; }

        [Required(ErrorMessage = "Введите полное имя")]
        public string Full_name { get; set; }

        [Required(ErrorMessage = "Введите возраст")]
        [Range(1, 100)]
        public int Age { get; set; }

        [Required(ErrorMessage = "Введите номер телефона")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Не валидный номер телефона")]
        public string Phone_number { get; set; }
    }
}
