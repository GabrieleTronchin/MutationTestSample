using System;
using System.Collections.Generic;
using System.Linq;

namespace Payment.Users
{
    public static class UserMemoryRepository
    {
        private static Dictionary<string, User> _users = new Dictionary<string, User>();

        public static User FindUser(string id)
        {
            return _users.TryGetValue(id, out User user) ? user : null;
        }

        public static void Save(User user)
        {
            _users[user.Id] = user;
        }

        public static void Delete(string id)
        {
            var user2Delete = FindUser(id);

            if (user2Delete == null)
                return;

            foreach (var user2 in user2Delete.Friends)
            {
                FindUser(user2.Id).Friends.Remove(user2Delete);
            }

            _users.Remove(id);
        }

        public static void AddFriend(string idUser1, string idUser2)
        {
            var user1 =
                FindUser(idUser1)
                ?? throw new InvalidOperationException(
                    $"System is not able to find any user with id={idUser1}"
                );
            var user2 =
                FindUser(idUser2)
                ?? throw new InvalidOperationException(
                    $"System is not able to find any user with id={idUser2}"
                );

            if (!user1.Friends.Contains(user2))
                user1.Friends.Add(user2);

            if (!user2.Friends.Contains(user1))
                user2.Friends.Add(user1);
        }

        public static IReadOnlyList<IUser> GetCommonFriends(string idUser1, string idUser2)
        {
            var user1 =
                FindUser(idUser1)
                ?? throw new InvalidOperationException(
                    $"System is not able to find any user with id={idUser1}"
                );
            var user2 =
                FindUser(idUser2)
                ?? throw new InvalidOperationException(
                    $"System is not able to find any user with id={idUser2}"
                );

            return user1.Friends.Intersect(user2.Friends).ToList();
        }
    }
}
