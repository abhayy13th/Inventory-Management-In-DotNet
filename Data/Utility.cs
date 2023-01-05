using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Data
{
    public class Utility
    {
        public static string GetAppDirectoryPath()
        {
            return @"D:\00Islington\Application Development\Tasks\CourseWork\wwwroot\Files";
        }

        public static string GetAppUsersFilePath()
        {
            
            return Path.Combine(GetAppDirectoryPath(), "users.json");
            
        }
        public static string GetAppRequestFilePath()
        {

            return Path.Combine(GetAppDirectoryPath(), "requests.json");

        }

        public static string GetAppLogFilePath()
        {

            return Path.Combine(GetAppDirectoryPath(), "logs.json");

        }


        public static string GetAppItemsFilePath()
        {
           
            return Path.Combine(GetAppDirectoryPath(), "items.json");

        }
    }


}

