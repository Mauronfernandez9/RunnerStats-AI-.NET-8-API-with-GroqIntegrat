using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace RunnerStats.Models.Entities
{
    public class User
    {
        [Key]
        public int IdUser { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "You must write a correct format email.")]
        public string Email { get; set; }
     
        public string PasswordHash { get; set; }
        public int RunnerId { get; set; }

        public Runner Runner { get; set; }


    }
}
