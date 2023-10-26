using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebApplication1.Models
{
    public class User: IdentityUser
    {

        public virtual ICollection<UserVoyage> UserVoyages { get; set; }
           
    }
}
