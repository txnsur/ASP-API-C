namespace GardenAPI.Models
{
    public class Venta
    {
        public int ID { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal FinalPrice { get; set; }
        public int ClientID { get; set; }
        public int SensorPackID { get; set; }
        public bool Status { get; set; }
    }
}
