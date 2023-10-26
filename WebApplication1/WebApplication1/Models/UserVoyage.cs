namespace WebApplication1.Models
{
    public class UserVoyage
    {
        public int Id { get; set; }


        public int TripUserId { get; set; }
        public virtual TripUser TripUser { get; set; }


        public int VoyageId { get; set; }
        public virtual Voyage Voyage { get; set;}
    }
}
