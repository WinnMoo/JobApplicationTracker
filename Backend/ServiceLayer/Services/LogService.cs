using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft;
using Newtonsoft.Json;
using ServiceLayer.Interfaces;
using ServiceLayer.Models;

namespace ServiceLayer
{
    public class LogService: ILogService
    {
        
        enum OperatingSystems { Windows, MacOS, Linux }

        const OperatingSystems OPERATINGSYSTEM = OperatingSystems.MacOS;


        public LogService()
        {

        }

        /*
         * Creates a new log for today's date
         * Returns bool for whether or not log was created
         */
        public bool NewLog()
        {
            //only create a new log if log is not up to date
            var wasLogCreated = false;

            string today = DateTime.Today.ToString();

            today = today.Replace('/', '-'); //Converts the date time string into system compatible format
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string logFolderPath = Path.Combine(desktopPath, "logs");
            string logFilePath = Path.Combine(logFolderPath, today + "json");


            switch (OPERATINGSYSTEM)
            {
                case OperatingSystems.Windows:
                    break;
                case OperatingSystems.MacOS:

                    if(File.Exists(logFilePath))
                    {
                        wasLogCreated = false;
                    }
                    else
                    {
                        wasLogCreated = CreateLog(logFilePath);
                    }
                    break;
                case OperatingSystems.Linux:
                    break;
                default:
                    break;
            }

            return wasLogCreated;
        }

        public bool CreateLog(string filePath)
        {
            var wasLogCreated = false;
            List<Log> logs = new List<Log>();
            try
            {
                using (StreamWriter file = File.CreateText(filePath))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, logs);
                }
                wasLogCreated = true;
            }
            catch
            {
                return wasLogCreated;
            }
            return wasLogCreated;

        }

        public bool AddLog(Log logToAdd)
        {
            var wasLogAdded = false;
            string today = DateTime.Today.ToString();

            today = today.Replace('/', '-'); //Converts the date time string into system compatible format
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string logFolderPath = Path.Combine(desktopPath, "logs");
            string logFilePath = Path.Combine(logFolderPath, today + "json");

            List<Log> logs;

            using (StreamReader reader = new StreamReader(logFilePath))
            {
                string json = reader.ReadToEnd();
                logs = JsonConvert.DeserializeObject<List<Log>>(json);
                
            };

            logs.Add(logToAdd);

            try
            {
                File.WriteAllText(logFilePath, JsonConvert.SerializeObject(logs));
                wasLogAdded = true;
            }
            catch
            {
                return wasLogAdded;
            }
           

            return wasLogAdded;
        }
    }
}
