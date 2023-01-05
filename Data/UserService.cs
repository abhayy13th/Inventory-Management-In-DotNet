
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Security.Cryptography;
using CourseWork.Data.Model;

using System.Net.NetworkInformation;
using System.Xml;
using System.Data;

namespace CourseWork.Data
{
    public class UserService
    {
        public const string SeedUsername = "admin";
        public const string SeedPassword = "admin";

        public void SeedUser()
		{
            var users = GetAll().FirstOrDefault(x => x.Role == RoleModel.Admin);

            if (users == null)
            {
                var user = new User()
                {
                    Id = Guid.NewGuid(),
                    Username = SeedUsername,
                    PasswordHash = Helper.ConvertHash(SeedPassword, null),
                    Role = RoleModel.Admin
                };

                var allUsers = GetAll();
                allUsers.Add(user);
                SaveAll(allUsers);
            }
                 
           

        }


        private static void SaveAll(List<User> users)
        {
            string appDataDirectoryPath = Utility.GetAppDirectoryPath();
            string appUsersFilePath = Utility.GetAppUsersFilePath();

            if (!Directory.Exists(appDataDirectoryPath))
            {
                Directory.CreateDirectory(appDataDirectoryPath);
            }

            var json = JsonSerializer.Serialize(users);
            File.WriteAllText(appUsersFilePath, json);

        }
        public static List<User> GetAll()
        {
            string appUsersFilePath = Utility.GetAppUsersFilePath();
            if (!File.Exists(appUsersFilePath))
            {
                return new List<User>();
            }

            var json = File.ReadAllText(appUsersFilePath);

            return JsonSerializer.Deserialize<List<User>>(json);


        }
        
        public static User Register(string username, string password, RoleModel role)
        {
            List<User> users = GetAll();
            bool usernameExists = users.Any(x => x.Username == username);
            if (usernameExists)
            {
             
                throw new Exception("Username already exists.");
                
            }
            var user = new User{
                Username = username,
                PasswordHash = Helper.ConvertHash(password, null),
                Role = role
            };
            users.Add(user);
            SaveAll(users);
            return user;
           

     
        }

        public static User Login(string username, string password)
        {
            var users = GetAll();
            var user = users.FirstOrDefault(x => x.Username == username);
            if (user == null)
            {
                return null;
            }
            var passwordHash = Helper.ConvertHash(password, null);
            return user;
        }
        



    }
}
