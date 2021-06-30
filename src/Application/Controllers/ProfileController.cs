using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using dotnetServer.Domain.Respositories;
using dotnetServer.Domain.DTOs;
using dotnetServer.Services.ProfileServices;
using dotnetServer.Application.Exceptions;

namespace dotnetServer.Application.Controllers
{
    [ApiController]
    [Route("profiles")]
    public class ProfileController: ControllerBase
    {
        // [HttpGet]
        // [Route("")]
        // public async Task<ActionResult<IEnumerable<PublicProfileDTO>>> Get([FromServices] IProfileRepository profileRepository){
        //     var profiles = (await profileRepository.FindAll()).Select(PublicProfileDTO.FromProfile);
        //     return Ok(profiles);
        // }
        [HttpGet]
        [Route("{username}")]
        public async Task<ActionResult<PublicProfileDTO>> Get(string username, [FromServices] IProfileRepository profileRepository){
            var profile = await profileRepository.Find(p => p.Username == username);
            return Ok(PublicProfileDTO.FromProfile(profile));
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<PublicProfileDTO>> Post([FromBody] CreateProfileDTO profileDTO, [FromServices] CreateProfileService createProfileService)
        {
            if (!ModelState.IsValid)
            {
                throw new FieldModelException(ModelState);
            }
            var profile = await createProfileService.Run(profileDTO);
            return PublicProfileDTO.FromProfile(profile);
        }
    }
}