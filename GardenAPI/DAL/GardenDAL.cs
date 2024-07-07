// DAL/GardenDAL.cs
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using GardenAPI.Models;
using GardenAPI.DataAccess;

namespace GardenAPI.DAL
{
    public class GardenDAL
    {
        public void CreateGarden(Garden garden)
        {
            string query = @"
                INSERT INTO JARDINES (name, longitude, latitude, userID, sensorPackID, status)
                VALUES (@Name, @Longitude, @Latitude, @UserID, @SensorPackID, 1);
            ";

            SqlParameter[] parameters = {
                new SqlParameter("@Name", garden.Name),
                new SqlParameter("@Longitude", garden.Longitude),
                new SqlParameter("@Latitude", garden.Latitude),
                new SqlParameter("@UserID", garden.UserID),
                new SqlParameter("@SensorPackID", garden.SensorPackID)
            };

            SqlServerConnection.ExecuteNonQuery(query, parameters);
        }

        public Garden GetGardenById(int gardenId)
        {
            string query = "SELECT * FROM JARDINES WHERE ID = @GardenId AND status = 1";
            SqlParameter[] parameters = { new SqlParameter("@GardenId", gardenId) };

            DataTable result = SqlServerConnection.ExecuteQuery(query, parameters);

            if (result.Rows.Count == 0) return null;

            DataRow row = result.Rows[0];
            return new Garden
            {
                ID = Convert.ToInt32(row["ID"]),
                Name = Convert.ToString(row["name"]),
                Longitude = Convert.ToDecimal(row["longitude"]),
                Latitude = Convert.ToDecimal(row["latitude"]),
                UserID = Convert.ToInt32(row["userID"]),
                SensorPackID = Convert.ToInt32(row["sensorPackID"])
            };
        }

        public List<Garden> GetAllGardens()
        {
            List<Garden> gardens = new List<Garden>();
            string query = "SELECT * FROM JARDINES WHERE status = 1";

            DataTable result = SqlServerConnection.ExecuteQuery(query);

            foreach (DataRow row in result.Rows)
            {
                Garden garden = new Garden
                {
                    ID = Convert.ToInt32(row["ID"]),
                    Name = Convert.ToString(row["name"]),
                    Longitude = Convert.ToDecimal(row["longitude"]),
                    Latitude = Convert.ToDecimal(row["latitude"]),
                    UserID = Convert.ToInt32(row["userID"]),
                    SensorPackID = Convert.ToInt32(row["sensorPackID"])
                };
                gardens.Add(garden);
            }

            return gardens;
        }

        public void UpdateGarden(Garden garden)
        {
            string query = @"
                UPDATE JARDINES
                SET name = @Name,
                    longitude = @Longitude,
                    latitude = @Latitude,
                    userID = @UserID,
                    sensorPackID = @SensorPackID
                WHERE ID = @GardenId;
            ";

            SqlParameter[] parameters = {
                new SqlParameter("@GardenId", garden.ID),
                new SqlParameter("@Name", garden.Name),
                new SqlParameter("@Longitude", garden.Longitude),
                new SqlParameter("@Latitude", garden.Latitude),
                new SqlParameter("@UserID", garden.UserID),
                new SqlParameter("@SensorPackID", garden.SensorPackID)
            };

            SqlServerConnection.ExecuteNonQuery(query, parameters);
        }

        public void DeleteGarden(int gardenId)
        {
            string query = @"
                UPDATE JARDINES
                SET status = 0
                WHERE ID = @GardenId;
            ";

            SqlParameter[] parameters = { new SqlParameter("@GardenId", gardenId) };

            SqlServerConnection.ExecuteNonQuery(query, parameters);
        }
    }
}
