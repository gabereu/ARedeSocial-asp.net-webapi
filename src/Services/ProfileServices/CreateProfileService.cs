using System.Threading.Tasks;
using dotnetServer.Domain.DTOs.ProfileDTO;
using dotnetServer.Domain.Models;
using dotnetServer.Domain.Respositories;
using dotnetServer.Services.AuthorizationServices;
using Microsoft.AspNetCore.Mvc;

namespace dotnetServer.Services.ProfileServices
{
    public class CreateProfileService
    {
        private readonly IProfileRepository _profileRepository;
        private readonly EncryptionService _encryptionService;
        public CreateProfileService([FromServices] IProfileRepository profileRepository, [FromServices] EncryptionService encryptionService)
        {
            _profileRepository = profileRepository;
            _encryptionService = encryptionService;
        }
        public async Task<Profile> Run(CreateProfileDTO profileDTO){
            profileDTO.Password = _encryptionService.EncryptPassword(profileDTO.Password);
            var profile = await _profileRepository.Create(profileDTO);
            return profile;
        }
    }
}