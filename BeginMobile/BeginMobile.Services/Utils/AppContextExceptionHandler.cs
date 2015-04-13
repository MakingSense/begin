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
        private const string DefaultTitle = "Error";

        public const string NamedMessage = "AppContextError";
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
                MessagingCenter.Send(new AppContextError(DefaultTitle, exception.Message, "Ok"), NamedMessage);
            }

            else
            {
                throw new AppContextException(exception.Message);
            }
        }
        public static void Send(Exception exception, BaseServiceError serviceError, ExceptionLevel exceptionLevel)
        {
            if (exceptionLevel == ExceptionLevel.Application || exceptionLevel == ExceptionLevel.PageView)
            {
                MessageSendConfigure(exception, serviceError);
            }

            else
            {
                throw new AppContextException(exception.Message);
            }
        }
        private static void MessageSendConfigure(Exception exception, BaseServiceError serviceError)
        {
            if (serviceError != null)
            {
                if (serviceError.HasError)
                {
                    if (serviceError.Errors.Any())
                    {
                        MessagingCenter.Send(new AppContextError(DefaultTitle,
                            serviceError.Errors.Aggregate(string.Empty,
                                (current, error) => current + (error.ErrorMessage + "\n")),
                            "Ok"), NamedMessage);
                    }

                    else if (!string.IsNullOrEmpty(serviceError.Error))
                    {
                        MessagingCenter.Send(new AppContextError(DefaultTitle, serviceError.Error, "Ok"), NamedMessage);
                    }
                }

                else
                {
                    MessagingCenter.Send(new AppContextError(DefaultTitle, exception.Message, "Ok"), NamedMessage);
                }
            }

            else
            {
                MessagingCenter.Send(new AppContextError(DefaultTitle, exception.Message, "Ok"), NamedMessage);
            }
        }
    }
}