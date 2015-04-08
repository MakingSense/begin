using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BeginMobile.Utils
{
    public class FriendshipActions: ContentPage{ }

    public enum Friendship
    {
        Send = 0,
        Cancel = 1,
        Accept = 2,
        Reject = 3,
        Remove = 4
    }
}
