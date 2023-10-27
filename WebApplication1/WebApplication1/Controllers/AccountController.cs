using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        UserManager<TripUser> userManager;
        IConfiguration configuration;

        public AccountController(UserManager<TripUser> userManager)
        {
            this.userManager = userManager;
            //this.configuration = configuration;
        }

        //POST api/Account/Login
        [HttpPost]
        public async Task<ActionResult> Login(LoginDTO login)
        {
            TripUser user = await userManager.FindByNameAsync(login.UserName);

            //Si l'utilisateur a fonctionné
            if (user != null && await userManager.CheckPasswordAsync(user, login.Password))
            {
                IList<string> Roles = await userManager.GetRolesAsync(user);

                List<Claim> authClaims = new List<Claim>();

                foreach (string role in Roles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }
                authClaims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));

                SymmetricSecurityKey authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Ma_clé_secrète_ici_elle_se_trouve"));

                JwtSecurityToken token = new JwtSecurityToken(
                    issuer: "https://locahost:7024",
                    audience: "http://localhost:4200",
                    claims: authClaims,
                    //On indique le temps que le token est gardé
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256)
                );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    validTo = token.ValidTo
                });
            }
            

            return StatusCode(StatusCodes.Status417ExpectationFailed, new { Message = "L'utilisateur est introuvable." });
            
        }

        //POST api/Account/Register
        [HttpPost]
        public async Task<ActionResult> Register(RegisterDTO register)
        {
            //On fait la validation du Client côté Serveur.
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
                return StatusCode(StatusCodes.Status500InternalServerError, new {Error = identityResult.Errors});
            }

            return Ok();
        }
    }
}
