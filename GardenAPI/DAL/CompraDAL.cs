// DAL/CompraDAL.cs
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using GardenAPI.Models;
using GardenAPI.DataAccess;

namespace GardenAPI.DAL
{
    public class CompraDAL
    {
        public void CreateCompra(Compra compra)
        {
            string query = @"
                INSERT INTO COMPRAS (quantity, totalPrice, purchaseDate, adminID, supplierID, sensorPackID, status)
                VALUES (@Quantity, @TotalPrice, @PurchaseDate, @AdminID, @SupplierID, @SensorPackID, 1);
            ";

            SqlParameter[] parameters = {
                new SqlParameter("@Quantity", compra.Quantity),
                new SqlParameter("@TotalPrice", compra.TotalPrice),
                new SqlParameter("@PurchaseDate", compra.PurchaseDate),
                new SqlParameter("@AdminID", compra.AdminID),
                new SqlParameter("@SupplierID", compra.SupplierID),
                new SqlParameter("@SensorPackID", compra.SensorPackID)
            };

            SqlServerConnection.ExecuteNonQuery(query, parameters);
        }

        public Compra GetCompraById(int compraId)
        {
            string query = "SELECT * FROM COMPRAS WHERE ID = @CompraId AND status = 1";
            SqlParameter[] parameters = { new SqlParameter("@CompraId", compraId) };

            DataTable result = SqlServerConnection.ExecuteQuery(query, parameters);

            if (result.Rows.Count == 0) return null;

            DataRow row = result.Rows[0];
            return new Compra
            {
                ID = Convert.ToInt32(row["ID"]),
                Quantity = Convert.ToInt32(row["quantity"]),
                TotalPrice = Convert.ToDecimal(row["totalPrice"]),
                PurchaseDate = Convert.ToDateTime(row["purchaseDate"]),
                AdminID = Convert.ToInt32(row["adminID"]),
                SupplierID = Convert.ToInt32(row["supplierID"]),
                SensorPackID = Convert.ToInt32(row["sensorPackID"])
            };
        }

        public List<Compra> GetAllCompras()
        {
            List<Compra> compras = new List<Compra>();
            string query = "SELECT * FROM COMPRAS WHERE status = 1";

            DataTable result = SqlServerConnection.ExecuteQuery(query);

            foreach (DataRow row in result.Rows)
            {
                Compra compra = new Compra
                {
                    ID = Convert.ToInt32(row["ID"]),
                    Quantity = Convert.ToInt32(row["quantity"]),
                    TotalPrice = Convert.ToDecimal(row["totalPrice"]),
                    PurchaseDate = Convert.ToDateTime(row["purchaseDate"]),
                    AdminID = Convert.ToInt32(row["adminID"]),
                    SupplierID = Convert.ToInt32(row["supplierID"]),
                    SensorPackID = Convert.ToInt32(row["sensorPackID"])
                };
                compras.Add(compra);
            }

            return compras;
        }

        public void UpdateCompra(Compra compra)
        {
            string query = @"
                UPDATE COMPRAS
                SET quantity = @Quantity,
                    totalPrice = @TotalPrice,
                    purchaseDate = @PurchaseDate,
                    adminID = @AdminID,
                    supplierID = @SupplierID,
                    sensorPackID = @SensorPackID
                WHERE ID = @CompraID;
            ";

            SqlParameter[] parameters = {
                new SqlParameter("@Quantity", compra.Quantity),
                new SqlParameter("@TotalPrice", compra.TotalPrice),
                new SqlParameter("@PurchaseDate", compra.PurchaseDate),
                new SqlParameter("@AdminID", compra.AdminID),
                new SqlParameter("@SupplierID", compra.SupplierID),
                new SqlParameter("@SensorPackID", compra.SensorPackID),
                new SqlParameter("@CompraID", compra.ID)
            };

            SqlServerConnection.ExecuteNonQuery(query, parameters);
        }

        public void DeleteCompra(int compraId)
        {
            string query = @"
                UPDATE COMPRAS
                SET status = 0
                WHERE ID = @CompraId;
            ";

            SqlParameter[] parameters = { new SqlParameter("@CompraId", compraId) };

            SqlServerConnection.ExecuteNonQuery(query, parameters);
        }
    }
}
