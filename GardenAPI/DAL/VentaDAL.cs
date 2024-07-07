using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using GardenAPI.Models;
using GardenAPI.DataAccess;

namespace GardenAPI.DAL
{
    public class VentaDAL
    {
        public void CreateVenta(Venta venta)
        {
            string query = @"
                INSERT INTO VENTAS (purchaseDate, finalPrice, clientID, sensorPackID, status)
                VALUES (@PurchaseDate, @FinalPrice, @ClientID, @SensorPackID, 1);
            ";

            SqlParameter[] parameters = {
                new SqlParameter("@PurchaseDate", venta.PurchaseDate),
                new SqlParameter("@FinalPrice", venta.FinalPrice),
                new SqlParameter("@ClientID", venta.ClientID),
                new SqlParameter("@SensorPackID", venta.SensorPackID)
            };

            SqlServerConnection.ExecuteNonQuery(query, parameters);
        }

        public Venta GetVentaById(int ventaId)
        {
            string query = "SELECT * FROM VENTAS WHERE ID = @VentaId AND status = 1";
            SqlParameter[] parameters = { new SqlParameter("@VentaId", ventaId) };

            DataTable result = SqlServerConnection.ExecuteQuery(query, parameters);

            if (result.Rows.Count == 0) return null;

            DataRow row = result.Rows[0];
            return new Venta
            {
                ID = Convert.ToInt32(row["ID"]),
                PurchaseDate = Convert.ToDateTime(row["purchaseDate"]),
                FinalPrice = Convert.ToDecimal(row["finalPrice"]),
                ClientID = Convert.ToInt32(row["clientID"]),
                SensorPackID = Convert.ToInt32(row["sensorPackID"])
            };
        }

        public List<Venta> GetAllVentas()
        {
            List<Venta> ventas = new List<Venta>();
            string query = "SELECT * FROM VENTAS WHERE status = 1";

            DataTable result = SqlServerConnection.ExecuteQuery(query);

            foreach (DataRow row in result.Rows)
            {
                Venta venta = new Venta
                {
                    ID = Convert.ToInt32(row["ID"]),
                    PurchaseDate = Convert.ToDateTime(row["purchaseDate"]),
                    FinalPrice = Convert.ToDecimal(row["finalPrice"]),
                    ClientID = Convert.ToInt32(row["clientID"]),
                    SensorPackID = Convert.ToInt32(row["sensorPackID"])
                };
                ventas.Add(venta);
            }

            return ventas;
        }

        public void UpdateVenta(Venta venta)
        {
            string query = @"
                UPDATE VENTAS
                SET purchaseDate = @PurchaseDate,
                    finalPrice = @FinalPrice,
                    clientID = @ClientID,
                    sensorPackID = @SensorPackID
                WHERE ID = @VentaID;
            ";

            SqlParameter[] parameters = {
                new SqlParameter("@PurchaseDate", venta.PurchaseDate),
                new SqlParameter("@FinalPrice", venta.FinalPrice),
                new SqlParameter("@ClientID", venta.ClientID),
                new SqlParameter("@SensorPackID", venta.SensorPackID),
                new SqlParameter("@VentaID", venta.ID)
            };

            SqlServerConnection.ExecuteNonQuery(query, parameters);
        }

        public void DeleteVenta(int ventaId)
        {
            string query = @"
                UPDATE VENTAS
                SET status = 0
                WHERE ID = @VentaId;
            ";

            SqlParameter[] parameters = { new SqlParameter("@VentaId", ventaId) };

            SqlServerConnection.ExecuteNonQuery(query, parameters);
        }
    }
}
