using System.Diagnostics;
using BeginMobile.iOS.DependencyService;
using BeginMobile.Services.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof (Logging))]

namespace BeginMobile.iOS.DependencyService
{
    public class Logging : ILoggingService
    {
        private string FileName { get; set; }
        private StreamWriter _sw;

        public Logging()
        {
            try
            {
                var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                var cache = Path.Combine(documents, "..", "Library", "Caches");
                var logfile = Path.Combine(cache, @"log" + DateTime.Now.ToString("yyyymmdd") + ".log");
                FileName = logfile;
                const string str = "Xamarin App Logging Started\n";

                Debug.WriteLine(string.Format("Log to FileName '{0}'", FileName));

                _sw = new StreamWriter(FileName, true);
                Log(str);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.ToString());
                throw;
            }
            Debug.WriteLine("Logging started.");
        }

        public void Pause()
        {
            if (_sw == null) return;
            _sw.Flush();
            _sw.Close();
            _sw = null;
        }

        private void Log(string msg, string comment = "")
        {
            if (_sw == null)
                return;
            msg = DateTime.Now.ToString("T") + " : " + msg + (comment != "" ? " [" + comment + "]" : "");
            Task.Factory.StartNew(() =>
            {
                _sw.WriteLine(msg);
                Debug.WriteLine(msg);
            },
                TaskCreationOptions.LongRunning
                );
        }

        private void Dispose(Boolean disposing)
        {
            if (_sw != null)
            {
                _sw.Close();
                _sw = null;
            }
        }

        public void Dispose()
        {
            Dispose(true); //i am calling you from Dispose, it's safe
            GC.SuppressFinalize(this); //Hey, GC: don't bother calling finalize later
        }

        ~Logging()
        {
            Dispose(false); //i am *not* calling you from Dispose, it's *not* safe
        }

        public void Exception(Exception exception)
        {
            Log(exception.ToString(), "Exception");
        }

        public void Error(string message)
        {
            Log(message, "Error");
        }

        public void Warning(string message)
        {
            Log(message, "Warning");
        }

        public void Info(string message)
        {
            Log(message, "Info");
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
            Log(string.Format(errorFormat, parameters), "Error");
        }

        public void DebugFormat(string errorFormat, params object[] parameters)
        {
            Log(string.Format(errorFormat, parameters), "Debug");
        }
    }
}