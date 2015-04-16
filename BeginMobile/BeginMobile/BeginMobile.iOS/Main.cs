using System;
using System.Diagnostics;
using UIKit;

namespace BeginMobile.iOS
{
    public static class Application
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            try
            {
                UIApplication.Main(args, null, "AppDelegate");
            }

            catch (Exception exception)
            {
                //TODO log exception
                Debug.Print("MonoTouch App error: {0}", exception.Message);
                throw;
            }

        }
    }
}