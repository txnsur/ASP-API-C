namespace GardenAPI.Models
{
    public class Lectura
    {
        public int ID { get; set; }
        public string? Value { get; set; }
        public DateTime TimeStamp { get; set; }
        public int SensorID { get; set; }
    }
}
