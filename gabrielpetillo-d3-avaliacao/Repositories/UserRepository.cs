using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using gabrielpetillo_d3_avaliacao.Interfaces;
using gabrielpetillo_d3_avaliacao.Models;

namespace gabrielpetillo_d3_avaliacao.Repositories
{

    /// <summary>
    /// User entity manipulation repository
    /// </summary>
    internal class UserRepository : IUser
    {

        private readonly string dbConnnection = "Data source=LAPTOP-GABRIELP\\SQLSERVER; initial catalog=Users; user id=sa; pwd=admin123;";

        /// <summary>
        /// Seacrh all users
        /// </summary>
        public List<User> SearchAll()
        {
            List<User> lisUsers = new List<User>();

            using (SqlConnection conn = new SqlConnection(dbConnnection))
            {
                string querySearchAll = "SELECT IdUser, Name, Email, CONVERT(VARCHAR(1000), Password) AS Password  FROM Users";

                conn.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySearchAll, conn))
                {
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        User user = new User()
                        {
                            IdUser = rdr[0].ToString(),

                            Name = rdr["Name"].ToString(),

                            Email = rdr["Email"].ToString(),

                            Password = rdr["Password"].ToString(),

                        };

                        lisUsers.Add(user);
                    }
                }
                conn.Close();

            }

            return lisUsers;
        }

        /// <summary>
        /// Seacrh user by email
        /// </summary>
        public User SearchByEmail(string userEmail)
        {
            User user = null;

            using (SqlConnection conn = new SqlConnection(dbConnnection))
            {
                string querySelectByEmail = "DECLARE @Email VARCHAR(255) = '" + userEmail + "' ;" +
                    "SELECT IdUser, Name, Email, CONVERT(VARCHAR(1000), Password) AS Password  FROM Users WHERE Email = @Email;";


                conn.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectByEmail, conn))
                {
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        user = new User()
                        {
                            IdUser = rdr[0].ToString(),
                            Name = rdr["Name"].ToString(),
                            Email = rdr["Email"].ToString(),
                            Password = rdr["Password"].ToString(),
                        };

                    }

                }

                conn.Close();
            }
            return user;
        }

        /// <summary>
        /// Add new user
        /// </summary>
        /// <param name="user">User model</param>
        public bool Create(User user, string action)
        {
            bool success = false;
            using (SqlConnection conn = new SqlConnection(dbConnnection))
            {
                string queryInsert = "SET @IdUser = NEWID(); " +
                    "INSERT INTO Users (IdUser, Name, Email, Password) VALUES (@IdUser, @Name, @Email, @Password)";
                user.Password = Encoding.Default.GetString(Encoding.UTF8.GetBytes(user.Password));

                using (SqlCommand cmd = new SqlCommand(queryInsert, conn))
                {
                    cmd.Parameters.AddWithValue("@IdUser", user.IdUser);
                    cmd.Parameters.AddWithValue("@Name", user.Name);
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    cmd.Parameters.AddWithValue("@Password", user.Password);

                    conn.Open();

                    cmd.ExecuteNonQuery();
                }
                conn.Close();
                success = true;
            }
            return success;
        }

        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="user">User model</param>
        public bool Update(User user)
        {
            bool success = false;
            using (SqlConnection conn = new SqlConnection(dbConnnection))
            {
                string queryUpdateBody = ";DECLARE @IdUser uniqueidentifier = "
                    +"CAST(SUBSTRING('"+user.IdUser+ "', 1, 8) + '-' " +
                    "+ SUBSTRING('" + user.IdUser + "', 9, 4) + '-' " +
                    "+ SUBSTRING('" + user.IdUser + "', 13, 4) + '-' " +
                    "+ SUBSTRING('" + user.IdUser + "', 17, 4) + '-' " +
                    "+ SUBSTRING('" + user.IdUser + "', 21, 12);" + 
                    "UPDATE Users SET Name = @Name, Email = @Email, Password = @Password WHERE IdUser = @IdUser";
                user.Password = Encoding.Default.GetString(Encoding.UTF8.GetBytes(user.Password));

                using (SqlCommand cmd = new SqlCommand(queryUpdateBody, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", user.Name);
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    cmd.Parameters.AddWithValue("@Password", user.Password);

                    conn.Open();

                    cmd.ExecuteNonQuery();
                }
                conn.Close();
                success = true;

            }
            return success;
        }

        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="user">User model</param>
        public bool Delete(string idUser)
        {
            bool success = false;
            using (SqlConnection conn = new SqlConnection(dbConnnection))
            {
                string queryDelete = "DELETE FROM Users WHERE IdUser = @IdUser";

                using (SqlCommand cmd = new SqlCommand(queryDelete, conn))
                {
                    cmd.Parameters.AddWithValue("@IdUser", idUser);

                    conn.Open();

                    cmd.ExecuteNonQuery();
                }
                conn.Close();
                success = true;
            }
            return success;
        }



    }
}

