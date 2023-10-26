namespace WebApplication1.Models
{
    public class UserVoyage
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public int VoyageId { get; set; }

        public Voyage Voyage { get; set;}
    }
}
