namespace GardenAPI.Models
{
    public class Garden
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public int UserID { get; set; }
        public int SensorPackID { get; set; }
        public bool Status { get; set; }
    }
}
