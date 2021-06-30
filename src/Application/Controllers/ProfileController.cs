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
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<IEnumerable<PublicProfileDTO>>> Get([FromServices] IProfileRepository profileRepository){
            var profiles = (await profileRepository.FindAll()).Select(PublicProfileDTO.FromProfile);
            return Ok(profiles);
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<IEnumerable<PublicProfileDTO>>> Get([FromServices] IProfileRepository profileRepository, string id){
            var profile = await profileRepository.Find(p => p.Email == id);
            return Ok(profile);
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<PublicProfileDTO>> Post([FromServices] CreateProfileService createProfileService, [FromBody] CreateProfileDTO profileDTO)
        {
            if (!ModelState.IsValid)
            {
                throw new FieldModelException(ModelState);
            }
            var profile = await createProfileService.Run(profileDTO);
            return PublicProfileDTO.FromProfile(profile);
            // return BadRequest(ModelState);
        }
    }
}