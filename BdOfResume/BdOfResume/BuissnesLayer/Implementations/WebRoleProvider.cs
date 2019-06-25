using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace BdOfResume.BuissnesLayer.Implementations
{
    public class WebRoleProvider : RoleProvider
    {
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            string s = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=aspnet-BdOfResume-20190523124105;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            using (SqlConnection con = new SqlConnection(s))
            {
                con.Open();
                string SqlString = "SELECT RoleId FROM [AspNetUserRoles] WHERE UserId = (SELECT Id FROM [AspNetUsers] WHERE UserName = @My)";
                using (SqlCommand com = new SqlCommand(SqlString, con))
                {
                    com.Parameters.AddWithValue("@My", username);
                    using (SqlDataReader reader = com.ExecuteReader())
                    {
                        if (reader.Read())
                            return new string[] { reader[0].ToString() };
                    }
                }
                return null;
            }
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}