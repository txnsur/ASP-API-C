// DAL/PlantillaDAL.cs
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using GardenAPI.Models;
using GardenAPI.DataAccess;

namespace GardenAPI.DAL
{
    public class PlantillaDAL
    {
        public void CreatePlantilla(Plantilla plantilla)
        {
            string query = @"
                INSERT INTO PLANTILLAS (name, description, idealLight, idealTemperature, idealMoisture, status)
                VALUES (@Name, @Description, @IdealLight, @IdealTemperature, @IdealMoisture, 1);
            ";

            SqlParameter[] parameters = {
                new SqlParameter("@Name", plantilla.Name),
                new SqlParameter("@Description", plantilla.Description),
                new SqlParameter("@IdealLight", plantilla.IdealLight),
                new SqlParameter("@IdealTemperature", plantilla.IdealTemperature),
                new SqlParameter("@IdealMoisture", plantilla.IdealMoisture)
            };

            SqlServerConnection.ExecuteNonQuery(query, parameters);
        }

        public Plantilla GetPlantillaById(int plantillaId)
        {
            string query = "SELECT * FROM PLANTILLAS WHERE ID = @PlantillaId AND status = 1";
            SqlParameter[] parameters = { new SqlParameter("@PlantillaId", plantillaId) };

            DataTable result = SqlServerConnection.ExecuteQuery(query, parameters);

            if (result.Rows.Count == 0) return null;

            DataRow row = result.Rows[0];
            return new Plantilla
            {
                ID = Convert.ToInt32(row["ID"]),
                Name = row["name"].ToString(),
                Description = row["description"].ToString(),
                IdealLight = row["idealLight"].ToString(),
                IdealTemperature = row["idealTemperature"].ToString(),
                IdealMoisture = row["idealMoisture"].ToString()
            };
        }

        public List<Plantilla> GetAllPlantillas()
        {
            List<Plantilla> plantillas = new List<Plantilla>();
            string query = "SELECT * FROM PLANTILLAS WHERE status = 1";

            DataTable result = SqlServerConnection.ExecuteQuery(query);

            foreach (DataRow row in result.Rows)
            {
                Plantilla plantilla = new Plantilla
                {
                    ID = Convert.ToInt32(row["ID"]),
                    Name = row["name"].ToString(),
                    Description = row["description"].ToString(),
                    IdealLight = row["idealLight"].ToString(),
                    IdealTemperature = row["idealTemperature"].ToString(),
                    IdealMoisture = row["idealMoisture"].ToString()
                };

                plantillas.Add(plantilla);
            }

            return plantillas;
        }

        public void UpdatePlantilla(Plantilla plantilla)
        {
            string query = @"
                UPDATE PLANTILLAS
                SET name = @Name,
                    description = @Description,
                    idealLight = @IdealLight,
                    idealTemperature = @IdealTemperature,
                    idealMoisture = @IdealMoisture
                WHERE ID = @PlantillaID;
            ";

            SqlParameter[] parameters = {
                new SqlParameter("@Name", plantilla.Name),
                new SqlParameter("@Description", plantilla.Description),
                new SqlParameter("@IdealLight", plantilla.IdealLight),
                new SqlParameter("@IdealTemperature", plantilla.IdealTemperature),
                new SqlParameter("@IdealMoisture", plantilla.IdealMoisture),
                new SqlParameter("@PlantillaID", plantilla.ID)
            };

            SqlServerConnection.ExecuteNonQuery(query, parameters);
        }

        public void DeletePlantilla(int plantillaId)
        {
            string query = @"
                UPDATE PLANTILLAS
                SET status = 0
                WHERE ID = @PlantillaId;
            ";

            SqlParameter[] parameters = { new SqlParameter("@PlantillaId", plantillaId) };

            SqlServerConnection.ExecuteNonQuery(query, parameters);
        }
    }
}
