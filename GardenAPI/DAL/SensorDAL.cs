// DAL/SensorDAL.cs
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using GardenAPI.Models;
using GardenAPI.DataAccess;

namespace GardenAPI.DAL
{
    public class SensorDAL
    {
        public void CreateSensor(Sensor sensor)
        {
            string query = @"
                INSERT INTO SENSORES (type, status)
                VALUES (@Type, @Status);
            ";

            SqlParameter[] parameters = {
                new SqlParameter("@Type", sensor.Type),
                new SqlParameter("@Status", sensor.Status)
            };

            SqlServerConnection.ExecuteNonQuery(query, parameters);
        }

        public Sensor GetSensorById(int sensorId)
        {
            string query = "SELECT * FROM SENSORES WHERE ID = @SensorId AND status = 1";
            SqlParameter[] parameters = { new SqlParameter("@SensorId", sensorId) };

            DataTable result = SqlServerConnection.ExecuteQuery(query, parameters);

            if (result.Rows.Count == 0) return null;

            DataRow row = result.Rows[0];
            return new Sensor
            {
                ID = Convert.ToInt32(row["ID"]),
                Type = Convert.ToString(row["type"]),
                Status = Convert.ToBoolean(row["status"])
            };
        }

        public List<Sensor> GetAllSensors()
        {
            List<Sensor> sensors = new List<Sensor>();
            string query = "SELECT * FROM SENSORES WHERE status = 1";

            DataTable result = SqlServerConnection.ExecuteQuery(query);

            foreach (DataRow row in result.Rows)
            {
                Sensor sensor = new Sensor
                {
                    ID = Convert.ToInt32(row["ID"]),
                    Type = row["type"].ToString(),
                    Status = Convert.ToBoolean(row["status"])
                };

                sensors.Add(sensor);
            }

            return sensors;
        }

        public void UpdateSensor(Sensor sensor)
        {
            string query = @"
                UPDATE SENSORES
                SET type = @Type,
                    status = @Status
                WHERE ID = @SensorId;
            ";

            SqlParameter[] parameters = {
                new SqlParameter("@SensorId", sensor.ID),
                new SqlParameter("@Type", sensor.Type),
                new SqlParameter("@Status", sensor.Status)
            };

            SqlServerConnection.ExecuteNonQuery(query, parameters);
        }

        public void DeleteSensor(int sensorId)
        {
            string query = @"
                UPDATE SENSORES
                SET status = 0
                WHERE ID = @SensorId;
            ";

            SqlParameter[] parameters = { new SqlParameter("@SensorId", sensorId) };

            SqlServerConnection.ExecuteNonQuery(query, parameters);
        }
    }
}
