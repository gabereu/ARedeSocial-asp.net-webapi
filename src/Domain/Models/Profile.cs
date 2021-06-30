using System;

namespace dotnetServer.Domain.Models
{
    public class Profile
    {
        public Guid Id {get; set;}
        public string Username {get; set;}
        public string Email {get; set;}
        public string PasswordHash {get; set;}
        public string Name {get; set;}
        public DateTime BirthDay {get; set;}
        public string Biography {get; set;}
    }
}