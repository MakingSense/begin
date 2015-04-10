using System;
using System.Linq;
using BeginMobile.Services.DTO;
using Xamarin.Forms;

namespace BeginMobile.Services.Utils
{
    public enum ExceptionLevel
    {
        System = 0,
        Application = 1,
        PageView = 2
    }
    public class AppContextException : Exception
    {
        public AppContextException(string message)
            : base(message)
        { }
    }

    public class AppContextError
    {
        public string Message { get; private set; }
        public string Title { get; private set; }
        public string Accept { get; private set; }

        private AppContextError(string title, string message, string accept)
        {
            Title = title;
            Message = message;
            Accept = accept;
        }

        public static void Send(Exception exception, ExceptionLevel exceptionLevel)
        {
            if (exceptionLevel == ExceptionLevel.Application || exceptionLevel == ExceptionLevel.PageView)
            {
                MessagingCenter.Send(new AppContextError("Error", exception.Message, "Ok"), "AppContextError");
            }

            else
            {
                throw new AppContextException(exception.Message);
            }
        }

        public static void Send(Exception exception, BaseServiceError serviceError, ExceptionLevel exceptionLevel)
        {
            if (serviceError != null)
            {
                if (serviceError.HasError)
                {
                    //TODO: Send ServiceError logic here
                }
            }
        }
    }
}