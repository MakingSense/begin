using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using BeginMobile.LocalizeResources.Resources;
using BeginMobile.Services.DTO;
using BeginMobile.Services.Interfaces;
using BeginMobile.Services.Logging;
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
        private static readonly ILoggingService _log = Logger.Current;

        private AppContextError(string title, string message, string accept)
        {
            Title = title;
            Message = message;
            Accept = accept;
        }
        public static void Send(string objectName, string method, Exception exception, ExceptionLevel exceptionLevel)
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
        public static void Send(string objectName, string method, Exception exception, BaseServiceError serviceError, ExceptionLevel exceptionLevel)
        {
            if (exceptionLevel == ExceptionLevel.Application || exceptionLevel == ExceptionLevel.PageView)
            {
                MessageSendConfigure(objectName, method,exception, serviceError);
            }

            else
            {
                throw new AppContextException(exception.Message);
            }
        }
        private static void MessageSendConfigure(string objectName, string method, Exception exception, BaseServiceError serviceError)
        {
            const string oldValue = "Error: ";
            if (serviceError != null)
            {
                if (serviceError.HasError)
                {
                    if (serviceError.Errors != null && serviceError.Errors.Any())
                    {                        
                        var errorMessages = ErrorMessages.GetTranslatedErrors(serviceError.Errors).Aggregate("", (current, error) => current + (error + "\n"));

                        var errorFormat = new AppContextError(DefaultTitle, errorMessages.Replace(oldValue,string.Empty),
                            "Ok");

                        MessagingCenter.Send(errorFormat, NamedMessage);
                        MessagingCenter.Unsubscribe<AppContextError>(errorFormat, NamedMessage);
                    }

                    else if (!string.IsNullOrEmpty(serviceError.Error))
                    {
                        if (exception.GetType() == typeof (WebException))
                        {
                            var ex = exception as WebException;
                            if (ex.Status.Equals(WebExceptionStatus.ConnectFailure))
                            {
                                var error = new AppContextError("Connection Error",
                                    "Connect Failure (Connection timed out)".Replace(oldValue, string.Empty), "Ok");
                                    //TODO:to resources
                                MessagingCenter.Send(error, NamedMessage);
                                MessagingCenter.Unsubscribe<AppContextError>(error, NamedMessage);
                            }
                        }
                        else
                        {
                            _log.Error("Log Error in " + objectName + " - " + method + " - " + exception.Message);
                        }                        
                    }
                }

                else
                {
                    _log.Error("Log Error in " + objectName + " - " + method + " - " + exception.Message);
                    if (exception.GetType() == typeof(WebException))
                    {
                        var e = exception as WebException;
                        if (e.Status.Equals(WebExceptionStatus.ConnectFailure))
                        {
                            var error = new AppContextError("Connection Error", "Connect Failure (Connection timed out)".Replace(oldValue, string.Empty), "Ok");//TODO:
                            MessagingCenter.Send(error, NamedMessage);
                            MessagingCenter.Unsubscribe<AppContextError>(error, NamedMessage);
                        }
                    }

                }
            }

            else
            {
                _log.Error("Log Error in "+objectName + " - " + method + " - " + exception.Message);
            }
        }
    }


    public class ErrorMessages
    {
        public static string ErrorLabel;

        public static List<string>GetTranslatedErrors(List<ServiceError> errors)
        {
            List<string> resultErrors= new List<string>();
            foreach (var error in errors)
            {
                var errorCode = error.ErrorCode;
                switch (errorCode)
                {
                    case ErrorCode.NotificationNoExists:
                        resultErrors.Add(AppResources.ErrorCodeNotificationNoExists);
                        break;
                    case ErrorCode.LoginUsernameWrong:
                        resultErrors.Add("User Name should not be empty.");//TODO:Add to Resources
                        break;
                    case ErrorCode.LoginPasswordWrong:
                        resultErrors.Add("Password should not be empty.");//TODO:Add to Resources
                        break;
                    case ErrorCode.LoginEmailLoginError:
                        resultErrors.Add("Invalid username and/or password.");//TODO:Add to Resources
                        break;
                    case ErrorCode.ChangePasswordEmailEmpty:
                        resultErrors.Add("Password is empty.");//TODO:Add to Resources
                        break;
                    case ErrorCode.ChangePasswordNewPasswordEmpty:
                        resultErrors.Add("New password is empty.");//TODO:Add to Resources
                        break;
                    case ErrorCode.ChangePasswordRepeatPasswordEmpty:
                        resultErrors.Add("Repeat password is empty.");//TODO:Add to Resources
                        break;
                    case ErrorCode.ChangePasswordNoMatch:
                        resultErrors.Add("Both new passwords are not identical.");//TODO:Add to Resources
                        break;
                    case ErrorCode.RegisterEmailWrong:
                        resultErrors.Add("Email has wrong format.");//TODO:Add to Resources
                        break;
                    case ErrorCode.RegisterNameSurname:
                        resultErrors.Add("Name & surname is empty.");//TODO:Add to Resources
                        break;
                   
                }
            }
            return resultErrors;
        }
    }

    public  static class ErrorCode
    {
        public const string NotificationNoExists = "notification_notification_no_exists";
        //login
        public const string LoginUsernameWrong = "username_wrong";
        public const string LoginPasswordWrong = "password_wrong";
        public const string LoginEmailLoginError = "email_login_error";
        //ChangePassword
        public const string ChangePasswordEmailEmpty = "password_empty";
        public const string ChangePasswordNewPasswordEmpty = "new_password_empty";
        public const string ChangePasswordRepeatPasswordEmpty = "repeat_password_empty";
        public const string ChangePasswordNoMatch = "new_password_no_match";
        //Register
        public const string RegisterEmailWrong = "email_wrong";
        public const string RegisterNameSurname = "name_surname";
    }
}