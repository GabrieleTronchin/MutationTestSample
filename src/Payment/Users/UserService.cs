using System;
using System.Collections.Generic;

namespace Payment.Users
{
    public class UserService : IUserService
    {
        public IUser GetUser(string id)
        {
            return UserMemoryRepository.FindUser(id);
        }

        public IUser CreateUser(string name)
        {
            var user = new User(name);
            UserMemoryRepository.Save(user);
            return user;
        }

        public void DeleteUser(IUser user)
        {
            if (user == null) throw new ArgumentException("User must be valid.");

            UserMemoryRepository.Delete(user.Id);
        }

        public void AddFriendship(IUser user1, IUser user2)
        {
            if (user1 == null) throw new ArgumentException("Both users must be valid.");
            if (user2 == null) throw new ArgumentException("Both users must be valid.");

            UserMemoryRepository.AddFriend(user1.Id, user2.Id);
        }
        public IReadOnlyList<IUser> GetCommonFriends(IUser user1, IUser user2)
        {
            if (user1 == null) throw new ArgumentException("Both users must be valid.");
            if (user2 == null) throw new ArgumentException("Both users must be valid.");
            return UserMemoryRepository.GetCommonFriends(user1.Id, user2.Id);
        }
        public IReadOnlyList<IUser> GetConnectionList(IUser user1, IUser user2, uint maxLevel)
        {
            if (user1 == null) throw new ArgumentException("Both users must be valid.");
            if (user2 == null) throw new ArgumentException("Both users must be valid.");
            
            if (user1 == user2) return new List<IUser> { user1 };

            Dictionary<IUser, int> distances = new();
            Dictionary<IUser, IUser> predecessors = new();
            Queue<IUser> queue = new();

            distances[user1] = 0;
            queue.Enqueue(user1);

            while (queue.Count > 0)
            {
                var currentUser = queue.Dequeue();

                if (currentUser == user2) break; // We found the destination user

                foreach (var friend in currentUser.Friends)
                {
                    if (!distances.ContainsKey(friend))
                    {
                        distances[friend] = distances[currentUser] + 1;
                        predecessors[friend] = currentUser;
                        queue.Enqueue(friend);
                    }
                }
            }

            if (!distances.ContainsKey(user2)) return new List<IUser>(); // No path found

            var path = new List<IUser>();
            var current = user2;

            while (current != user1)
            {
                path.Insert(0, current);
                current = predecessors[current];
            }

            path.Insert(0, user1);

            if (path.Count - 1 > maxLevel)
                return new List<IUser>(); // Path exceeds the specified maxLevel


            return path;
        }



    }
}