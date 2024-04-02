using NUnit.Framework;
using Payment.Users;
using System.Collections.Generic;
using System.Linq;

namespace Payment.Test.Users
{
    [TestFixture]
    public class GetConnectionTests
    {
        [Test]
        public void Test_GetConnectionList_With_Not_Connected_Users()
        {
            IUserService sut = new UserService();
            var user1 = sut.CreateUser("John Doe");
            var user2 = sut.CreateUser("Sara Mc Cain");
            AddFriends(user1, CreateUsers(10, sut), sut);
            user1.Friends.ToList().ForEach(f => AddFriends(f, CreateUsers(10, sut), sut));
            AddFriends(user2, CreateUsers(10, sut), sut);
            user2.Friends.ToList().ForEach(f => AddFriends(f, CreateUsers(10, sut), sut));

            CollectionAssert.IsEmpty(sut.GetConnectionList(user1, user2, 100));
        }

        [Test]
        public void Test_GetConnectionList_With_Friend_Users()
        {
            IUserService sut = new UserService();
            var user1 = sut.CreateUser("John Doe");
            var user2 = sut.CreateUser("Sara Mc Cain");
            sut.AddFriendship(user1, user2);

            CollectionAssert.AreEqual(new List<IUser> { user1, user2 }, sut.GetConnectionList(user1, user2, 100));
        }

        [Test]
        public void Test_GetConnectionList_With_Users_Having_Connection()
        {
            IUserService sut = new UserService();
            var user1 = sut.CreateUser("John Doe");
            var user2 = sut.CreateUser("Sara Mc Cain");
            var users = CreateUsers(4, sut);

            sut.AddFriendship(user1, users.First());
            sut.AddFriendship(users.First(), users[1]);
            sut.AddFriendship(users[1], users[2]);
            sut.AddFriendship(users[2], users[3]);
            sut.AddFriendship(users[3], user2);

            var expectedConnection = new List<IUser> { user1 };
            expectedConnection.AddRange(users);
            expectedConnection.Add(user2);

            CollectionAssert.AreEqual(expectedConnection, sut.GetConnectionList(user1, user2, 100));
        }

        [Test]
        public void Test_GetConnectionList_Should_Return_Shortest_Connection()
        {
            IUserService sut = new UserService();
            var user1 = sut.CreateUser("John Doe");
            var user2 = sut.CreateUser("Sara Mc Cain");
            var users = CreateUsers(4, sut);

            sut.AddFriendship(user1, users.First());
            sut.AddFriendship(users.First(), users[1]);
            sut.AddFriendship(users[1], users[2]);
            sut.AddFriendship(users[2], users[3]);
            sut.AddFriendship(users[3], user2);

            var bestFriend = sut.CreateUser("Best Friend");
            var coolFriend = sut.CreateUser("Cool Friend");
            sut.AddFriendship(user1, bestFriend);
            sut.AddFriendship(bestFriend, coolFriend);
            sut.AddFriendship(coolFriend, user2);

            var expectedConnection = new List<IUser> { user1, bestFriend, coolFriend, user2 };

            CollectionAssert.AreEqual(expectedConnection, sut.GetConnectionList(user1, user2, 100));
        }

        [Test]
        public void Test_GetConnectionList_Should_Return_Empty_List_If_MaxLevel_IsReached()
        {
            IUserService sut = new UserService();
            var user1 = sut.CreateUser("John Doe");
            var user2 = sut.CreateUser("Sara Mc Cain");
            var users = CreateUsers(4, sut);

            sut.AddFriendship(user1, users.First());
            sut.AddFriendship(users.First(), users[1]);
            sut.AddFriendship(users[1], users[2]);
            sut.AddFriendship(users[2], users[3]);
            sut.AddFriendship(users[3], user2);

            CollectionAssert.IsEmpty(sut.GetConnectionList(user1, user2, 3));
        }



        private List<IUser> CreateUsers(int nbUsers, IUserService userService)
        {
            return Enumerable.Range(1, nbUsers)
                .Select(i => userService.CreateUser($"user{i}"))
                .ToList();
        }

        private void AddFriends(IUser user, IList<IUser> friends, IUserService userService)
        {
            foreach (var friend in friends)
            {
                userService.AddFriendship(user, friend);
            }
        }
    }
}
