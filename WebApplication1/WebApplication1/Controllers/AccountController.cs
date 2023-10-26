using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        UserManager<TripUser> userManager;

        public AccountController(UserManager<TripUser> userManager)
        {
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterDTO register)
        {
            if(register.Password != register.PassewordConfirm)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mesaage = "Les 2 mots de passe ne concordent pas." });
            }

            TripUser user = new TripUser() //Pour créer un utilisateur
            {
                UserName = register.UserName,
                Email = register.Email
            };

            IdentityResult identityResult = await this.userManager.CreateAsync(user, register.Password);

            if (!identityResult.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok();
        }
    }
}
