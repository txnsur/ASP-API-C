// DAL/ProveedorDAL.cs
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using GardenAPI.Models;
using GardenAPI.DataAccess;

namespace GardenAPI.DAL
{
    public class ProveedorDAL
    {
        public void CreateProveedor(Proveedor proveedor)
        {
            string query = @"
                INSERT INTO PROVEEDORES (name, email, phoneNumber, country, zip, state, city, street, status)
                VALUES (@Name, @Email, @PhoneNumber, @Country, @Zip, @State, @City, @Street, 1);
            ";

            SqlParameter[] parameters = {
                new SqlParameter("@Name", proveedor.Name),
                new SqlParameter("@Email", proveedor.Email),
                new SqlParameter("@PhoneNumber", proveedor.PhoneNumber),
                new SqlParameter("@Country", proveedor.Country),
                new SqlParameter("@Zip", proveedor.Zip),
                new SqlParameter("@State", proveedor.State),
                new SqlParameter("@City", proveedor.City),
                new SqlParameter("@Street", proveedor.Street)
            };

            SqlServerConnection.ExecuteNonQuery(query, parameters);
        }

        public Proveedor GetProveedorById(int proveedorId)
        {
            string query = "SELECT * FROM PROVEEDORES WHERE ID = @ProveedorId AND status = 1";
            SqlParameter[] parameters = { new SqlParameter("@ProveedorId", proveedorId) };

            DataTable result = SqlServerConnection.ExecuteQuery(query, parameters);

            if (result.Rows.Count == 0) return null;

            DataRow row = result.Rows[0];
            return new Proveedor
            {
                ID = Convert.ToInt32(row["ID"]),
                Name = row["name"].ToString(),
                Email = row["email"].ToString(),
                PhoneNumber = row["phoneNumber"].ToString(),
                Country = row["country"].ToString(),
                Zip = row["zip"].ToString(),
                State = row["state"].ToString(),
                City = row["city"].ToString(),
                Street = row["street"].ToString()
            };
        }

        public List<Proveedor> GetAllProveedores()
        {
            List<Proveedor> proveedores = new List<Proveedor>();
            string query = "SELECT * FROM PROVEEDORES WHERE status = 1";

            DataTable result = SqlServerConnection.ExecuteQuery(query);

            foreach (DataRow row in result.Rows)
            {
                Proveedor proveedor = new Proveedor
                {
                    ID = Convert.ToInt32(row["ID"]),
                    Name = row["name"].ToString(),
                    Email = row["email"].ToString(),
                    PhoneNumber = row["phoneNumber"].ToString(),
                    Country = row["country"].ToString(),
                    Zip = row["zip"].ToString(),
                    State = row["state"].ToString(),
                    City = row["city"].ToString(),
                    Street = row["street"].ToString()
                };

                proveedores.Add(proveedor);
            }

            return proveedores;
        }

        public void UpdateProveedor(Proveedor proveedor)
        {
            string query = @"
                UPDATE PROVEEDORES
                SET name = @Name,
                    email = @Email,
                    phoneNumber = @PhoneNumber,
                    country = @Country,
                    zip = @Zip,
                    state = @State,
                    city = @City,
                    street = @Street
                WHERE ID = @ProveedorId;
            ";

            SqlParameter[] parameters = {
                new SqlParameter("@ProveedorId", proveedor.ID),
                new SqlParameter("@Name", proveedor.Name),
                new SqlParameter("@Email", proveedor.Email),
                new SqlParameter("@PhoneNumber", proveedor.PhoneNumber),
                new SqlParameter("@Country", proveedor.Country),
                new SqlParameter("@Zip", proveedor.Zip),
                new SqlParameter("@State", proveedor.State),
                new SqlParameter("@City", proveedor.City),
                new SqlParameter("@Street", proveedor.Street)
            };

            SqlServerConnection.ExecuteNonQuery(query, parameters);
        }

        public void DeleteProveedor(int proveedorId)
        {
            string query = @"
                UPDATE PROVEEDORES
                SET status = 0
                WHERE ID = @ProveedorId;
            ";

            SqlParameter[] parameters = { new SqlParameter("@ProveedorId", proveedorId) };

            SqlServerConnection.ExecuteNonQuery(query, parameters);
        }
    }
}
