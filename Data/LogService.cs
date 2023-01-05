using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CourseWork.Data.Model;
namespace CourseWork.Data
{
    internal class LogService
    {
        
       
        public static List<Log> AddLog(string message,DateTime dt)
        {
            
            List<Log> log = GetAllLogs();
            log.Add(new Log
            {
                LogEntry = message,
                datetime = dt
            });
            SaveLog(log);
            return log;
        }
        public static void SaveLog(List<Log> messages)
        {
            string appDataDirectoryPath = Utility.GetAppDirectoryPath();
            string LogFilePath = Utility.GetAppLogFilePath();

            if (!Directory.Exists(appDataDirectoryPath))
            {
                Directory.CreateDirectory(appDataDirectoryPath);
            }
            
            var json = JsonSerializer.Serialize(messages);
            File.WriteAllText(LogFilePath, json);

        }
        public static List<Log> GetAllLogs()
        {
            string appLogFilePath = Utility.GetAppLogFilePath();
            if (!File.Exists(appLogFilePath))
            {
                return new List<Log>();
            }

            var json = File.ReadAllText(appLogFilePath);

            return JsonSerializer.Deserialize<List<Log>>(json);
        }
        
        
    }
}
