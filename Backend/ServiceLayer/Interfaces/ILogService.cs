using System;
using ServiceLayer.Models;

namespace ServiceLayer.Interfaces
{
    public interface ILogService
    {
        bool NewLog();
        bool CreateLog(string filePath);
        bool AddLog(Log logToAdd);
    }
}
