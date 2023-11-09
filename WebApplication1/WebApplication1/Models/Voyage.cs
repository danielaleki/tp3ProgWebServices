using MessagePack;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApplication1.Models
{
    public class Voyage
    {
        
        public int Id { get; set; }

        [MaxLength(200)]
        public string Name { get; set; }

        public string PhotoCouverture { get; set; } 

        public Voyage() {
            PhotoCouverture = "/wwwroot/images/Safari.jpg"; //j'initialise la photo à cette image par défaut
        }

        public bool IsPublic { get; set; }

        [JsonIgnore]
        public virtual List<TripUser>? TripUsers { get; set; }
    }
}
