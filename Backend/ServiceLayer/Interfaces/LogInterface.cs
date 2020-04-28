using System;
using ServiceLayer.Models;

namespace ServiceLayer.Interfaces
{
    public interface LogInterface
    {
        bool NewLog();
        bool CreateLog(string filePath);
        bool AddLog(Log logToAdd);
    }
}
