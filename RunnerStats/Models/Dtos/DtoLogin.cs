using System.ComponentModel.DataAnnotations;
namespace RunnerStats.Models.Dtos
{
    public class DtoLogin
    {
        [Required(ErrorMessage ="Email is required")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [MinLength(8,ErrorMessage ="Password must be at least 8 characters long.")]
        [RegularExpression(@"^(?=.*[a-zA-Z])(?=.*\d)(?=.*[^a-zA-Z0-9]).+$",
            ErrorMessage = "Password must include at least one letter, one number, and one special character.")]
        public string? Password { get; set; }

    }
}
