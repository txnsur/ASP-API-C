namespace GardenAPI.Models
{
    public class UserMembership
    {
        public int ID { get; set; }
        public string? Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ClientID { get; set; }
        public int MembershipID { get; set; }
    }
}
