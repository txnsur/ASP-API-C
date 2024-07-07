namespace GardenAPI.Models
{
    public class Compra
    {
        public int ID { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int AdminID { get; set; }
        public int SupplierID { get; set; }
        public int SensorPackID { get; set; }
        public bool Status { get; set; }
    }
}
