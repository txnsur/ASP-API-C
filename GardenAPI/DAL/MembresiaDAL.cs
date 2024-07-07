// DAL/MembresiaDAL.cs
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using GardenAPI.Models;
using GardenAPI.DataAccess;

namespace GardenAPI.DAL
{
    public class MembresiaDAL
    {
        public void CreateMembresia(Membresia membresia)
        {
            string query = @"
                INSERT INTO MEMBRESIAS (name, description, durationDays, price, status)
                VALUES (@Name, @Description, @DurationDays, @Price, 1);
            ";

            SqlParameter[] parameters = {
                new SqlParameter("@Name", membresia.Name),
                new SqlParameter("@Description", membresia.Description),
                new SqlParameter("@DurationDays", membresia.DurationDays),
                new SqlParameter("@Price", membresia.Price)
            };

            SqlServerConnection.ExecuteNonQuery(query, parameters);
        }

        public Membresia GetMembresiaById(int membresiaId)
        {
            string query = "SELECT * FROM MEMBRESIAS WHERE ID = @MembresiaId AND status = 1";
            SqlParameter[] parameters = { new SqlParameter("@MembresiaId", membresiaId) };

            DataTable result = SqlServerConnection.ExecuteQuery(query, parameters);

            if (result.Rows.Count == 0) return null;

            DataRow row = result.Rows[0];
            return new Membresia
            {
                ID = Convert.ToInt32(row["ID"]),
                Name = row["name"].ToString(),
                Description = row["description"].ToString(),
                DurationDays = Convert.ToInt32(row["durationDays"]),
                Price = Convert.ToDecimal(row["price"])
            };
        }

        public List<Membresia> GetAllMembresias()
        {
            List<Membresia> membresias = new List<Membresia>();
            string query = "SELECT * FROM MEMBRESIAS WHERE status = 1";

            DataTable result = SqlServerConnection.ExecuteQuery(query);

            foreach (DataRow row in result.Rows)
            {
                Membresia membresia = new Membresia
                {
                    ID = Convert.ToInt32(row["ID"]),
                    Name = row["name"].ToString(),
                    Description = row["description"].ToString(),
                    DurationDays = Convert.ToInt32(row["durationDays"]),
                    Price = Convert.ToDecimal(row["price"])
                };

                membresias.Add(membresia);
            }

            return membresias;
        }

        public void UpdateMembresia(Membresia membresia)
        {
            string query = @"
                UPDATE MEMBRESIAS
                SET name = @Name,
                    description = @Description,
                    durationDays = @DurationDays,
                    price = @Price
                WHERE ID = @MembresiaID;
            ";

            SqlParameter[] parameters = {
                new SqlParameter("@Name", membresia.Name),
                new SqlParameter("@Description", membresia.Description),
                new SqlParameter("@DurationDays", membresia.DurationDays),
                new SqlParameter("@Price", membresia.Price),
                new SqlParameter("@MembresiaID", membresia.ID)
            };

            SqlServerConnection.ExecuteNonQuery(query, parameters);
        }

        public void DeleteMembresia(int membresiaId)
        {
            string query = @"
                UPDATE MEMBRESIAS
                SET status = 0
                WHERE ID = @MembresiaId;
            ";

            SqlParameter[] parameters = { new SqlParameter("@MembresiaId", membresiaId) };

            SqlServerConnection.ExecuteNonQuery(query, parameters);
        }
    }
}
