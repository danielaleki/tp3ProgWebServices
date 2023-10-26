using MessagePack;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Voyage
    {
        
        public int Id { get; set; }

        [MaxLength(200)]
        public string Name { get; set; }

        public string Picture { get; set; } //Image par défaut

        public bool IsPublic { get; set; }

        public virtual ICollection<UserVoyage> UserVoyages { get; set; }
    }
}
