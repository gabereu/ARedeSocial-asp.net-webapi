using System.ComponentModel.DataAnnotations;

namespace dotnetServer.Domain.DTOs.ProfileDTO
{
    public class LoginProfileDTO
    {
        [Required(ErrorMessage = "Field required")]
        public string Login {get; set;}

        [Required(ErrorMessage = "Field required")]
        public string Password {get; set;}
    }
}