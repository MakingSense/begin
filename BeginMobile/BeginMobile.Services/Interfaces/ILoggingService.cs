using System;

namespace BeginMobile.Services.Interfaces
{
    public interface ILoggingService : IDisposable
    {
        void Exception(Exception exception);//error

        void Error(string message);//error

        void Warning(string message);//warning

        void Info(string message);//verbose

        void Message(string message, LogLevel level);//error or warning or verbose

        void ErrorFormat(string errorFormat, params object[] parameters);

        void DebugFormat(string errorFormat, params object[] parameters);
    }
}