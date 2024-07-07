// DAL/UsuarioDAL.cs
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using GardenAPI.Models;
using GardenAPI.DataAccess;

namespace GardenAPI.DAL
{
    public class UsuarioDAL
    {
        public void CreateUser(Usuario user)
        {
            string query = @"
                INSERT INTO USUARIOS (username, password, email, role, firstName, lastName, street, city, state, zip, country, status)
                VALUES (@Username, @Password, @Email, @Role, @FirstName, @LastName, @Street, @City, @State, @Zip, @Country, 1);
            ";

            SqlParameter[] parameters = {
                new SqlParameter("@Username", user.Username),
                new SqlParameter("@Password", user.Password),
                new SqlParameter("@Email", user.Email),
                new SqlParameter("@Role", "User"), // Asignando automáticamente "User" al campo role
                new SqlParameter("@FirstName", user.FirstName ?? (object)DBNull.Value),
                new SqlParameter("@LastName", user.LastName ?? (object)DBNull.Value),
                new SqlParameter("@Street", user.Street ?? (object)DBNull.Value),
                new SqlParameter("@City", user.City ?? (object)DBNull.Value),
                new SqlParameter("@State", user.State ?? (object)DBNull.Value),
                new SqlParameter("@Zip", user.Zip ?? (object)DBNull.Value),
                new SqlParameter("@Country", user.Country ?? (object)DBNull.Value)
            };

            SqlServerConnection.ExecuteNonQuery(query, parameters);
        }

        public Usuario GetUserById(int userId)
        {
            string query = "SELECT * FROM USUARIOS WHERE ID = @UserId AND status = 1";
            SqlParameter[] parameters = {
                new SqlParameter("@UserId", userId)
            };

            DataTable result = SqlServerConnection.ExecuteQuery(query, parameters);

            if (result.Rows.Count == 0) return null;

            DataRow row = result.Rows[0];
            return new Usuario
            {
                ID = Convert.ToInt32(row["ID"]),
                Username = Convert.ToString(row["username"]),
                Password = Convert.ToString(row["password"]),
                Email = Convert.ToString(row["email"]),
                Role = Convert.ToString(row["role"]),
                FirstName = row["firstName"] != DBNull.Value ? Convert.ToString(row["firstName"]) : null,
                LastName = row["lastName"] != DBNull.Value ? Convert.ToString(row["lastName"]) : null,
                Street = row["street"] != DBNull.Value ? Convert.ToString(row["street"]) : null,
                City = row["city"] != DBNull.Value ? Convert.ToString(row["city"]) : null,
                State = row["state"] != DBNull.Value ? Convert.ToString(row["state"]) : null,
                Zip = row["zip"] != DBNull.Value ? Convert.ToString(row["zip"]) : null,
                Country = row["country"] != DBNull.Value ? Convert.ToString(row["country"]) : null
            };
        }

        public List<Usuario> GetAllUsers()
        {
            List<Usuario> users = new List<Usuario>();
            string query = "SELECT * FROM USUARIOS WHERE status = 1";

            DataTable result = SqlServerConnection.ExecuteQuery(query);

            foreach (DataRow row in result.Rows)
            {
                Usuario user = new Usuario
                {
                    ID = Convert.ToInt32(row["ID"]),
                    Username = Convert.ToString(row["username"]),
                    Password = Convert.ToString(row["password"]),
                    Email = Convert.ToString(row["email"]),
                    Role = Convert.ToString(row["role"]),
                    FirstName = row["firstName"] != DBNull.Value ? Convert.ToString(row["firstName"]) : null,
                    LastName = row["lastName"] != DBNull.Value ? Convert.ToString(row["lastName"]) : null,
                    Street = row["street"] != DBNull.Value ? Convert.ToString(row["street"]) : null,
                    City = row["city"] != DBNull.Value ? Convert.ToString(row["city"]) : null,
                    State = row["state"] != DBNull.Value ? Convert.ToString(row["state"]) : null,
                    Zip = row["zip"] != DBNull.Value ? Convert.ToString(row["zip"]) : null,
                    Country = row["country"] != DBNull.Value ? Convert.ToString(row["country"]) : null
                };

                users.Add(user);
            }

            return users;
        }

        public void UpdateUser(Usuario user)
        {
            string query = @"
                UPDATE USUARIOS
                SET username = @Username,
                    password = @Password,
                    email = @Email,
                    role = @Role,
                    firstName = @FirstName,
                    lastName = @LastName,
                    street = @Street,
                    city = @City,
                    state = @State,
                    zip = @Zip,
                    country = @Country
                WHERE ID = @UserId;
            ";

            SqlParameter[] parameters = {
                new SqlParameter("@UserId", user.ID),
                new SqlParameter("@Username", user.Username),
                new SqlParameter("@Password", user.Password),
                new SqlParameter("@Email", user.Email),
                new SqlParameter("@Role", user.Role ?? "User"), // Asignando "User" si no se proporciona un valor
                new SqlParameter("@FirstName", user.FirstName ?? (object)DBNull.Value),
                new SqlParameter("@LastName", user.LastName ?? (object)DBNull.Value),
                new SqlParameter("@Street", user.Street ?? (object)DBNull.Value),
                new SqlParameter("@City", user.City ?? (object)DBNull.Value),
                new SqlParameter("@State", user.State ?? (object)DBNull.Value),
                new SqlParameter("@Zip", user.Zip ?? (object)DBNull.Value),
                new SqlParameter("@Country", user.Country ?? (object)DBNull.Value)
            };

            SqlServerConnection.ExecuteNonQuery(query, parameters);
        }

        public void DeleteUser(int userId)
        {
            string query = "UPDATE USUARIOS SET status = 0 WHERE ID = @UserId";

            SqlParameter[] parameters = {
                new SqlParameter("@UserId", userId)
            };

            SqlServerConnection.ExecuteNonQuery(query, parameters);
        }
    }
}
