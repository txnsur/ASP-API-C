// DAL/UserTemplateDAL.cs
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using GardenAPI.Models;
using GardenAPI.DataAccess;
using System.Data;

namespace GardenAPI.DAL
{
    public class UserTemplateDAL
    {
        public void AddUserTemplate(int userId, int templateId, int gardenId)
        {
            string query = @"
                INSERT INTO USER_TEMPLATE (userID, templateID, gardenID, status)
                VALUES (@UserId, @TemplateId, @GardenId, 1);
            ";

            SqlParameter[] parameters = {
                new SqlParameter("@UserId", userId),
                new SqlParameter("@TemplateId", templateId),
                new SqlParameter("@GardenId", gardenId)
            };

            SqlServerConnection.ExecuteNonQuery(query, parameters);
        }

        public UserTemplate GetUserTemplate(int userId, int templateId)
        {
            string query = "SELECT * FROM USER_TEMPLATE WHERE userID = @UserId AND templateID = @TemplateId AND status = 1";
            SqlParameter[] parameters = {
                new SqlParameter("@UserId", userId),
                new SqlParameter("@TemplateId", templateId)
            };

            DataTable result = SqlServerConnection.ExecuteQuery(query, parameters);

            if (result.Rows.Count == 0) return null;

            DataRow row = result.Rows[0];
            return new UserTemplate
            {
                UserID = Convert.ToInt32(row["userID"]),
                TemplateID = Convert.ToInt32(row["templateID"]),
                GardenID = Convert.ToInt32(row["gardenID"])
            };
        }

        public List<UserTemplate> GetAllUserTemplates()
        {
            List<UserTemplate> userTemplates = new List<UserTemplate>();
            string query = "SELECT * FROM USER_TEMPLATE WHERE status = 1";

            DataTable result = SqlServerConnection.ExecuteQuery(query);

            foreach (DataRow row in result.Rows)
            {
                UserTemplate userTemplate = new UserTemplate
                {
                    UserID = Convert.ToInt32(row["userID"]),
                    TemplateID = Convert.ToInt32(row["templateID"]),
                    GardenID = Convert.ToInt32(row["gardenID"])
                };

                userTemplates.Add(userTemplate);
            }

            return userTemplates;
        }

        public void UpdateUserTemplate(UserTemplate userTemplate)
        {
            string query = @"
                UPDATE USER_TEMPLATE
                SET gardenID = @GardenId
                WHERE userID = @UserId AND templateID = @TemplateId;
            ";

            SqlParameter[] parameters = {
                new SqlParameter("@GardenId", userTemplate.GardenID),
                new SqlParameter("@UserId", userTemplate.UserID),
                new SqlParameter("@TemplateId", userTemplate.TemplateID)
            };

            SqlServerConnection.ExecuteNonQuery(query, parameters);
        }

        public void DeleteUserTemplate(int userId, int templateId)
        {
            string query = "UPDATE USER_TEMPLATE SET status = 0 WHERE userID = @UserId AND templateID = @TemplateId";

            SqlParameter[] parameters = {
                new SqlParameter("@UserId", userId),
                new SqlParameter("@TemplateId", templateId)
            };

            SqlServerConnection.ExecuteNonQuery(query, parameters);
        }
    }
}
