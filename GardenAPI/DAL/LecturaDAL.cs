// DAL/LecturaDAL.cs
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using GardenAPI.Models;
using GardenAPI.DataAccess;

namespace GardenAPI.DAL
{
    public class LecturaDAL
    {
        public void CreateLectura(Lectura lectura)
        {
            string query = @"
                INSERT INTO LECTURAS (value, timeStamp, sensorID)
                VALUES (@Value, @TimeStamp, @SensorID);
            ";

            SqlParameter[] parameters = {
                new SqlParameter("@Value", lectura.Value),
                new SqlParameter("@TimeStamp", lectura.TimeStamp),
                new SqlParameter("@SensorID", lectura.SensorID)
            };

            SqlServerConnection.ExecuteNonQuery(query, parameters);
        }

        public Lectura GetLecturaById(int lecturaId)
        {
            string query = "SELECT * FROM LECTURAS WHERE ID = @LecturaId";
            SqlParameter[] parameters = { new SqlParameter("@LecturaId", lecturaId) };

            DataTable result = SqlServerConnection.ExecuteQuery(query, parameters);

            if (result.Rows.Count == 0) return null;

            DataRow row = result.Rows[0];
            return new Lectura
            {
                ID = Convert.ToInt32(row["ID"]),
                Value = row["value"].ToString(),
                TimeStamp = Convert.ToDateTime(row["timeStamp"]),
                SensorID = Convert.ToInt32(row["sensorID"])
            };
        }

        public List<Lectura> GetAllLecturas()
        {
            List<Lectura> lecturas = new List<Lectura>();
            string query = "SELECT * FROM LECTURAS";

            DataTable result = SqlServerConnection.ExecuteQuery(query);

            foreach (DataRow row in result.Rows)
            {
                Lectura lectura = new Lectura
                {
                    ID = Convert.ToInt32(row["ID"]),
                    Value = row["value"].ToString(),
                    TimeStamp = Convert.ToDateTime(row["timeStamp"]),
                    SensorID = Convert.ToInt32(row["sensorID"])
                };

                lecturas.Add(lectura);
            }

            return lecturas;
        }

        public void UpdateLectura(Lectura lectura)
        {
            string query = @"
                UPDATE LECTURAS
                SET value = @Value,
                    timeStamp = @TimeStamp,
                    sensorID = @SensorID
                WHERE ID = @LecturaId;
            ";

            SqlParameter[] parameters = {
                new SqlParameter("@LecturaId", lectura.ID),
                new SqlParameter("@Value", lectura.Value),
                new SqlParameter("@TimeStamp", lectura.TimeStamp),
                new SqlParameter("@SensorID", lectura.SensorID)
            };

            SqlServerConnection.ExecuteNonQuery(query, parameters);
        }

        public void DeleteLectura(int lecturaId)
        {
            string query = "DELETE FROM LECTURAS WHERE ID = @LecturaId";
            SqlParameter[] parameters = { new SqlParameter("@LecturaId", lecturaId) };

            SqlServerConnection.ExecuteNonQuery(query, parameters);
        }
    }
}
