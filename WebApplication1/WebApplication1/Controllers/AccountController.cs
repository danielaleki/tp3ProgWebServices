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
        IConfiguration config;
        ILogger<AccountController> _logger;

        public AccountController(UserManager<TripUser> userManager, IConfiguration configuration, ILogger<AccountController> logger)
        {
            this.userManager = userManager;
            this.config = configuration;
            _logger = logger;
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

                SymmetricSecurityKey authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Secret"]));

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
            

            return StatusCode(StatusCodes.Status417ExpectationFailed, new { Message = "L'utilisateur est introuvable ou le mot de passe " +
              "et le nom d'utilisateur sont introuvables." });
            
        }

        //POST api/Account/Register
        [HttpPost]
        public async Task<ActionResult> Register(RegisterDTO register)
        {
            //On fait la validation du Client côté Serveur.
            if(register.Password != register.PasswordConfirm)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Les 2 mots de passe ne concordent pas." });
            }

            TripUser user = new TripUser() //Pour créer un utilisateur
            {
                UserName = register.UserName,
                Email = register.Email
            };

            IdentityResult identityResult = await this.userManager.CreateAsync(user, register.Password);

            if (!identityResult.Succeeded)
            {
                //Pour retrouver un log de l'erreur dans la console
                var errors = identityResult.Errors.Select(e => e.Description);
                _logger.LogError(String.Join(",", errors));

                return StatusCode(StatusCodes.Status500InternalServerError, new {Error = identityResult.Errors});
            }

            return Ok();
        }
    }
}
