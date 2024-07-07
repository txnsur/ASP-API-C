// DAL/SensorPackDAL.cs
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using GardenAPI.Models;
using GardenAPI.DataAccess;

namespace GardenAPI.DAL
{
    public class SensorPackDAL
    {
        public void CreateSensorPack(SensorPack sensorPack)
        {
            string query = @"
                INSERT INTO SENSOR_PACKS (purchasePrice, salePrice, status)
                VALUES (@PurchasePrice, @SalePrice, 1);
            ";

            SqlParameter[] parameters = {
                new SqlParameter("@PurchasePrice", sensorPack.PurchasePrice),
                new SqlParameter("@SalePrice", sensorPack.SalePrice)
            };

            SqlServerConnection.ExecuteNonQuery(query, parameters);
        }

        public SensorPack GetSensorPackById(int sensorPackId)
        {
            string query = "SELECT * FROM SENSOR_PACKS WHERE ID = @SensorPackId AND status = 1";
            SqlParameter[] parameters = { new SqlParameter("@SensorPackId", sensorPackId) };

            DataTable result = SqlServerConnection.ExecuteQuery(query, parameters);

            if (result.Rows.Count == 0) return null;

            DataRow row = result.Rows[0];
            return new SensorPack
            {
                ID = Convert.ToInt32(row["ID"]),
                PurchasePrice = Convert.ToDecimal(row["purchasePrice"]),
                SalePrice = Convert.ToDecimal(row["salePrice"])
            };
        }

        public List<SensorPack> GetAllSensorPacks()
        {
            List<SensorPack> sensorPacks = new List<SensorPack>();
            string query = "SELECT * FROM SENSOR_PACKS WHERE status = 1";

            DataTable result = SqlServerConnection.ExecuteQuery(query);

            foreach (DataRow row in result.Rows)
            {
                SensorPack sensorPack = new SensorPack
                {
                    ID = Convert.ToInt32(row["ID"]),
                    PurchasePrice = Convert.ToDecimal(row["purchasePrice"]),
                    SalePrice = Convert.ToDecimal(row["salePrice"])
                };

                sensorPacks.Add(sensorPack);
            }

            return sensorPacks;
        }

        public void UpdateSensorPack(SensorPack sensorPack)
        {
            string query = @"
                UPDATE SENSOR_PACKS
                SET purchasePrice = @PurchasePrice,
                    salePrice = @SalePrice
                WHERE ID = @SensorPackId;
            ";

            SqlParameter[] parameters = {
                new SqlParameter("@SensorPackId", sensorPack.ID),
                new SqlParameter("@PurchasePrice", sensorPack.PurchasePrice),
                new SqlParameter("@SalePrice", sensorPack.SalePrice)
            };

            SqlServerConnection.ExecuteNonQuery(query, parameters);
        }

        public void DeleteSensorPack(int sensorPackId)
        {
            string query = "UPDATE SENSOR_PACKS SET status = 0 WHERE ID = @SensorPackId";
            SqlParameter[] parameters = { new SqlParameter("@SensorPackId", sensorPackId) };

            SqlServerConnection.ExecuteNonQuery(query, parameters);
        }
    }
}
