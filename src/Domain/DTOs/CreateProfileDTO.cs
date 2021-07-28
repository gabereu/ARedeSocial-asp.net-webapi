using System;
using System.ComponentModel.DataAnnotations;
using dotnetServer.Domain.Models;

namespace dotnetServer.Domain.DTOs.ProfileDTO
{
    public class CreateProfileDTO
    {
        [Required(ErrorMessage = "Field required")]
        [MinLength(3, ErrorMessage = "Min 3 characteres")]
        public string Username {get; set;}

        [Required(ErrorMessage = "Field required")]
        [EmailAddress(ErrorMessage = "Email not valid")]
        public string Email {get; set;}

        [Required(ErrorMessage = "Field required")]
        public string Password {get; set;}

        [Required(ErrorMessage = "Field required")]
        public string Name {get; set;}

        [Required(ErrorMessage = "Field required")]
        public DateTime BirthDay {get; set;}
        public string Biography {get; set;}
        public Profile ToProfile(){
            return new Profile(){
                Username = this.Username,
                Email = this.Email,
                PasswordHash = this.Password,
                Name = this.Name,
                BirthDay = this.BirthDay,
                Biography = this.Biography,
            };
        }
    }
}