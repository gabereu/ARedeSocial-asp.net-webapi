using System.Threading.Tasks;
using dotnetServer.Domain.DTOs;
using dotnetServer.Domain.Models;
using dotnetServer.Domain.Respositories;
using Microsoft.AspNetCore.Mvc;

namespace dotnetServer.Services.ProfileServices
{
    public class CreateProfileService
    {
        private readonly IProfileRepository _profileRepository;
        public CreateProfileService([FromServices] IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }
        public async Task<Profile> Run(CreateProfileDTO profileDTO){
            
            var profile = await _profileRepository.Create(profileDTO);
            return profile;
        }
    }
}