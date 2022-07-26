using System;
using System.IO;
using System.Collections.Generic;
using gabrielpetillo_d3_avaliacao.Interfaces;

namespace gabrielpetillo_d3_avaliacao.Models
{
    /// <summary>
    /// User properties.
    /// </summary>
    internal class User : FileManipulation, IUser
    {
        public string IdUser { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        private const string path = "database/users.csv";

        /// <summary>
        /// In each User instance, verify folder and file existence
        /// </summary>
        public User()
        {
            CreateFolderAndFile(path);
        }

        /// <summary>
        /// Define CSV line structure
        /// </summary>
        /// <param name="user">Object with data do save</param>
        /// <returns>Returns prepared line to save</returns>
        private static string PrepareLine(User user, string action)
        {
            DateTime date = DateTime.Now;
            return $"{date:dd/MM/yyyy HH:mm};{action};{user.IdUser};{user.Name};{user.Email}";
        }

        /// <summary>
        /// List all users
        /// </summary>
        /// <returns>Returns users list</returns>
        public List<User> SearchAll()
        {
            List<User> users = new List<User>();

            string[] lines = File.ReadAllLines(path);

            foreach (var item in lines)
            {
                string[] line = item.Split(";");

                User user = new User()
                {
                    IdUser = line[0],
                    Name = line[1],
                    Email = line[2],
                    Password = line[3]
                };

                users.Add(user);
            }

            return users;
        }

        /// <summary>
        /// Creates new user
        /// </summary>
        /// <param name="user">Object with data to save</param>
        /// <param name="action">User action</param>
        public bool Create(User user, string action)
        {
            string[] line = { PrepareLine(user, action) };
            File.AppendAllLines(path, line);
            return true;
        }

        /// <summary>
        /// Edits an existing user
        /// </summary>
        /// <param name="user">Object with new data</param>
        public bool Update(User user)
        {
            List<string> lines = ReadAllLinesCSV(path);
            lines.RemoveAll(x => x.Split(";")[0] == user.IdUser);
            lines.Add(PrepareLine(user, "-"));
            RewriteCSV(path, lines);
            return true;
        }

        /// <summary>
        /// Deletes and existing user
        /// </summary>
        /// <param name="idUser">Id of user to be deleted</param>
        public bool Delete(string idUser)
        {
            List<string> lines = ReadAllLinesCSV(path);
            lines.RemoveAll(x => x.Split(";")[0] == idUser);
            RewriteCSV(path, lines);
            return true;
        }
    }
}
