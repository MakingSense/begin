using Android.Util;
using BeginMobile.Android.DependencyService;
using BeginMobile.Services.Interfaces;
using System;
using Xamarin.Forms;

[assembly: Dependency(typeof(Logging))]
namespace BeginMobile.Android.DependencyService
{
    public class Logging : ILoggingService
    {
        public void Exception(Exception exception)
        {
            Log.Error("Exception", "{0}", exception);
        }

        public void Error(string message)
        {
            Log.Error("Error", message);
        }

        public void Warning(string message)
        {
            Log.Warn("Warning", message);
        }

        public void Info(string message)
        {
            Log.Info("Info", message);
        }

        public void Message(string message, LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Error:
                    Error(message);
                    break;
                case LogLevel.Warning:
                    Warning(message);
                    break;
                case LogLevel.Verbose:
                    Info(message);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("level");
            }
        }

        public void ErrorFormat(string errorFormat, params object[] parameters)
        {
            Log.Error("Exception", errorFormat, parameters);
        }

        public void DebugFormat(string errorFormat, params object[] parameters)
        {
            Log.Debug("Debug", errorFormat, parameters);
        }

        public void Dispose()
        {

        }
    }
}