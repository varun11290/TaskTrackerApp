using System.ComponentModel.DataAnnotations;

namespace TaskTracker.API.DTO
{
    public class CreateUserDTO
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}