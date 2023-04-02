using System.ComponentModel.DataAnnotations;

namespace Asparagus.Domain.Models
{
    public class MessageDto
    {
        [Required(ErrorMessage = "Не указано имя")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Не указан email")]
        [EmailAddress(ErrorMessage = "Не корректный email")]
        [MaxLength(250)]
        public string Email { get; set; }

    }
}
