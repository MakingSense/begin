using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeginMobile.Services.ManagerServices
{
    public static class ConfigBaseAddress
    {
        public static string BaseAddress = "http://186.109.86.251:5432/";
        public static string SubAddress = "begin/api/v1/";


        //For ContactManager
        public static string IdentifierContancts = "contacts";
        public static string IdentifierAuxContacts = "users";

        //For EventManager
        public static string IdentifierEvents = "events";

        //For MessageManager
        public static string IdentifierMessages = "messages";

        //For NotificationManager
        public static string IdentifierNotifications = "notifications";

        //For GroupManager
        public static string IdentifierGroups = "groups";

        //For ProfileManager
        public static string IdentifierProfile = "profile";
        public static string IdentifierMe = "me";

    }
}
