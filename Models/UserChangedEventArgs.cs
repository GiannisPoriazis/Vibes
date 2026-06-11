using System;

namespace Vibes.Models
{
    public class UserChangedEventArgs : EventArgs
    {
        public UserInfo? User { get; }

        public UserChangedEventArgs(UserInfo? user)
        {
            User = user;
        }
    }
}
