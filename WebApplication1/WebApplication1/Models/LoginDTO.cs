using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class LoginDTO
    {
        [System.ComponentModel.DataAnnotations.Required]
        public string UserName { get; set; }

        
        [System.ComponentModel.DataAnnotations.Required]
        public string Password { get; set; }
       
    }
}
