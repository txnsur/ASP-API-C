namespace GardenAPI.Models
{
    public class Plantilla
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? IdealLight { get; set; }
        public string? IdealTemperature { get; set; }
        public string? IdealMoisture { get; set; }
        public bool Status { get; set; }
    }
}
