// DAL/UserMembershipDAL.cs
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using GardenAPI.Models;
using GardenAPI.DataAccess;

namespace GardenAPI.DAL
{
    public class UserMembershipDAL
    {
        public void CreateUserMembership(UserMembership userMembership)
        {
            string query = @"
                INSERT INTO USER_MEMBERSHIPS (status, startDate, endDate, clientID, membershipID)
                VALUES (@Status, @StartDate, @EndDate, @ClientID, @MembershipID);
            ";

            SqlParameter[] parameters = {
                new SqlParameter("@Status", userMembership.Status),
                new SqlParameter("@StartDate", userMembership.StartDate),
                new SqlParameter("@EndDate", userMembership.EndDate),
                new SqlParameter("@ClientID", userMembership.ClientID),
                new SqlParameter("@MembershipID", userMembership.MembershipID)
            };

            SqlServerConnection.ExecuteNonQuery(query, parameters);
        }

        public UserMembership GetUserMembershipById(int userMembershipId)
        {
            string query = "SELECT * FROM USER_MEMBERSHIPS WHERE ID = @UserMembershipId";
            SqlParameter[] parameters = { new SqlParameter("@UserMembershipId", userMembershipId) };

            DataTable result = SqlServerConnection.ExecuteQuery(query, parameters);

            if (result.Rows.Count == 0) return null;

            DataRow row = result.Rows[0];
            return new UserMembership
            {
                ID = Convert.ToInt32(row["ID"]),
                Status = row["status"].ToString(),
                StartDate = Convert.ToDateTime(row["startDate"]),
                EndDate = Convert.ToDateTime(row["endDate"]),
                ClientID = Convert.ToInt32(row["clientID"]),
                MembershipID = Convert.ToInt32(row["membershipID"])
            };
        }

        public List<UserMembership> GetAllUserMemberships()
        {
            List<UserMembership> userMemberships = new List<UserMembership>();
            string query = "SELECT * FROM USER_MEMBERSHIPS";

            DataTable result = SqlServerConnection.ExecuteQuery(query);

            foreach (DataRow row in result.Rows)
            {
                UserMembership userMembership = new UserMembership
                {
                    ID = Convert.ToInt32(row["ID"]),
                    Status = row["status"].ToString(),
                    StartDate = Convert.ToDateTime(row["startDate"]),
                    EndDate = Convert.ToDateTime(row["endDate"]),
                    ClientID = Convert.ToInt32(row["clientID"]),
                    MembershipID = Convert.ToInt32(row["membershipID"])
                };

                userMemberships.Add(userMembership);
            }

            return userMemberships;
        }

        public void UpdateUserMembership(UserMembership userMembership)
        {
            string query = @"
                UPDATE USER_MEMBERSHIPS
                SET status = @Status,
                    startDate = @StartDate,
                    endDate = @EndDate,
                    clientID = @ClientID,
                    membershipID = @MembershipID
                WHERE ID = @UserMembershipId;
            ";

            SqlParameter[] parameters = {
                new SqlParameter("@Status", userMembership.Status),
                new SqlParameter("@StartDate", userMembership.StartDate),
                new SqlParameter("@EndDate", userMembership.EndDate),
                new SqlParameter("@ClientID", userMembership.ClientID),
                new SqlParameter("@MembershipID", userMembership.MembershipID),
                new SqlParameter("@UserMembershipId", userMembership.ID)
            };

            SqlServerConnection.ExecuteNonQuery(query, parameters);
        }

        public void DeleteUserMembership(int userMembershipId)
        {
            string query = "DELETE FROM USER_MEMBERSHIPS WHERE ID = @UserMembershipId";
            SqlParameter[] parameters = { new SqlParameter("@UserMembershipId", userMembershipId) };

            SqlServerConnection.ExecuteNonQuery(query, parameters);
        }
    }
}
