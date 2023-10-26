using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class RegisterDTO
    {
        [System.ComponentModel.DataAnnotations.Required]
        public string UserName { get; set; }
        
        [System.ComponentModel.DataAnnotations.Required]
        [EmailAddress]
        public string Email { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string Password { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string PassewordConfirm { get; set; }
    }
}
