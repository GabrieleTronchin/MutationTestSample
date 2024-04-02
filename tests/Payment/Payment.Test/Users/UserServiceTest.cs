using NUnit.Framework;
using Payment.Users;
using System;
using System.Linq;

namespace Payment.Test.Users;

[TestFixture]

public class UserServiceTest
{


    [Test]
    public void DeleteUser_NotCreated()
    {
        IUserService sut = new UserService();
        var user1 = new User("Test");
        sut.DeleteUser(user1); //if not exist not return exception
        Assert.True(true);
    }


    [Test]
    public void GetConnectionList_SameUserPass()
    {
        IUserService sut = new UserService();
        var user1 = sut.CreateUser("John Doe");
        var lst = sut.GetConnectionList(user1, user1, 100);

        Assert.NotNull(lst);
        Assert.IsTrue(lst.Count == 1);
        Assert.True(lst[0].Id == user1.Id);
    }


    [Test]
    public void Test_CreateUser()
    {
        IUserService sut = new UserService();
        var createdUser = sut.CreateUser("John Doe");
        Assert.NotNull(createdUser);
        Assert.AreEqual("John Doe", createdUser.Name);
    }

    [Test]
    public void Test_GetUser()
    {
        IUserService sut = new UserService();
        var createdUser = sut.CreateUser("John Doe");

        var user = sut.GetUser(createdUser.Id);
        Assert.AreEqual(createdUser, user);
    }

    [Test]
    public void Test_AddFriendship()
    {
        IUserService sut = new UserService();
        var user1 = sut.CreateUser("John Doe");
        var user2 = sut.CreateUser("Sara Mc Cain");

        sut.AddFriendship(user1, user2);

        Assert.Contains(user2, user1.Friends.ToList());
    }

    [Test]
    public void Test_Friendship_Relation_Is_Mutual()
    {
        IUserService sut = new UserService();
        var user1 = sut.CreateUser("John Doe");
        var user2 = sut.CreateUser("Sara Mc Cain");

        sut.AddFriendship(user1, user2);

        Assert.Contains(user2, user1.Friends.ToList());
        Assert.Contains(user1, user2.Friends.ToList());
    }

    [Test]
    public void Test_Delete_User()
    {
        IUserService sut = new UserService();
        var user1 = sut.CreateUser("John Doe");
        sut.DeleteUser(user1);

        Assert.IsNull(sut.GetUser(user1.Id));
    }

    [Test]
    public void Test_Delete_User_Also_Delete_From_Friends()
    {
        IUserService sut = new UserService();
        var user1 = sut.CreateUser("John Doe");
        var user2 = sut.CreateUser("Sara Mc Cain");

        sut.AddFriendship(user1, user2);
        sut.DeleteUser(user2);

        user1 = sut.GetUser(user1.Id);

        CollectionAssert.DoesNotContain(user1.Friends.ToList(), user2);
    }

    [Test]
    public void Test_Get_CommonFriends()
    {
        IUserService sut = new UserService();
        var user1 = sut.CreateUser("John Doe");
        var user2 = sut.CreateUser("Sara Mc Cain");
        var friendsList = Enumerable.Range(1, 100)
                .Select(i => sut.CreateUser($"friend{i}"))
                .ToList();

        friendsList.Take(75).ToList().ForEach(x => sut.AddFriendship(user1, x));
        friendsList.Skip(25).Take(75).ToList().ForEach(x => sut.AddFriendship(user2, x));

        CollectionAssert.AreEquivalent(friendsList.Skip(25).Take(50), sut.GetCommonFriends(user1, user2));
    }
}