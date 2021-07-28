using System;
using dotnetServer.Domain.Models;

namespace dotnetServer.Domain.DTOs.ProfileDTO
{
    public class PublicProfileDTO
    {
        public Guid Id;
        public string Username {get; set;}
        public string Email {get; set;}
        public string Name {get; set;}
        public DateTime BirthDay {get; set;}
        public string Biography {get; set;}

        public static PublicProfileDTO FromProfile(Profile profile, bool withId = false){
            var publicProfileDTO = new PublicProfileDTO(){
                Username = profile.Username,
                Email = profile.Email,
                Name = profile.Name,
                BirthDay = profile.BirthDay,
                Biography = profile.Biography,
            };

            if(withId){
                publicProfileDTO.Id = profile.Id;
            }

            return publicProfileDTO;
        }
    }
}