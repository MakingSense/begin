//#define FriendshipEnable

using System.Collections.Generic;
using BeginMobile.Services.DTO;

namespace BeginMobile.Utils
{
    public enum FriendshipOption
    {
        Send = 0,
        Cancel = 1,
        Accept = 2,
        Reject = 3,
        Remove = 4
    }

    public static class FriendshipActions
    {
        /// <summary>
        /// Execute friendship action.
        /// </summary>/
        /// <param name="friendship"></param>
        /// <param name="authToken"></param>
        /// <param name="username"></param>
        public static List<ServiceError> Request(FriendshipOption friendship, string authToken, string username)
        {
            var responseErrors = new List<ServiceError>();

#if FriendshipEnable
            switch (friendship)
            {
                case FriendshipOption.Send:
                    responseErrors = BeginApplication.ProfileServices.SendRequest(authToken, username);
                    break;

                case FriendshipOption.Cancel:
                    //TODO: Cancel request here
                    //responseErrors = BeginApplication.ProfileServices.CancelFriendship(authToken, username);
                    break;

                case FriendshipOption.Accept:
                    responseErrors = BeginApplication.ProfileServices.AcceptRequest(authToken, username);
                    break;

                case FriendshipOption.Reject:
                    responseErrors = BeginApplication.ProfileServices.RejectRequest(authToken, username);
                    break;

                case FriendshipOption.Remove:
                    responseErrors = BeginApplication.ProfileServices.RemoveFriendship(authToken, username);
                    break;
            }
#endif

            return responseErrors;
        }
    }

    public static class FriendshipMessages
    {
        public const string DisplayAlert = "DisplayAlert";
        public const string RemoveContact = "RemoveContact";
        public const string AddContact = "AddContact";
    }
}