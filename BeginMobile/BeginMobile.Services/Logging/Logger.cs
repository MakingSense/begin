using BeginMobile.Services.Interfaces;

namespace BeginMobile.Services.Logging
{
    public static class Logger
    {
        public static ILoggingService Current { get; set; }
    }
}