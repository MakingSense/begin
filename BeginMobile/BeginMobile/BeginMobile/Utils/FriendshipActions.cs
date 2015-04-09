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
        public static List<ContactServiceError> Request(FriendshipOption friendship, string authToken, string username)
        {
            var responseErrors = new List<ContactServiceError>();

#if FriendshipEnable
            switch (friendship)
            {
                case FriendshipOption.Send:
                    responseErrors = App.ProfileServices.SendRequest(authToken, username);
                    break;

                case FriendshipOption.Cancel:
                    //TODO: Cancel request here
                    //responseErrors = App.ProfileServices.CancelFriendship(authToken, username);
                    break;

                case FriendshipOption.Accept:
                    responseErrors = App.ProfileServices.AcceptRequest(authToken, username);
                    break;

                case FriendshipOption.Reject:
                    responseErrors = App.ProfileServices.RejectRequest(authToken, username);
                    break;

                case FriendshipOption.Remove:
                    responseErrors = App.ProfileServices.RemoveFriendship(authToken, username);
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
    }
}