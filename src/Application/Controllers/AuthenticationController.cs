using System.Threading.Tasks;
using dotnetServer.Domain.DTOs.ProfileDTO;
using dotnetServer.Domain.Respositories;
using dotnetServer.Services.AuthorizationServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BCryptNet = BCrypt.Net;

namespace dotnetServer.Application.Controllers
{
    [Route("authentication")]
    public class AuthenticationController: ControllerBase
    {
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate(
            [FromBody] LoginProfileDTO login, 
            [FromServices] IProfileRepository profileRepository, 
            [FromServices] TokenService tokenService,
            [FromServices] EncryptionService encryptionService
        ){
            var profile = await profileRepository.Find(p => p.Username == login.Login || p.Email == login.Login);

            if (
                profile == null ||
                !encryptionService.isPassword(login.Password, profile.PasswordHash)
            )
                return NotFound(new { message = "Usuário ou senha inválidos" });


            // Gera o Token
            var token = tokenService.GenerateToken(profile);
            
            // Retorna os dados
            return new
            {
                profile = PublicProfileDTO.FromProfile(profile, withId: true),
                token = token
            };
        }
    }
}