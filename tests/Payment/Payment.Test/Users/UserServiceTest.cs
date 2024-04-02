using NUnit.Framework;
using Payment.Users;
using System;

namespace Payment.Test.Users;

[TestFixture]

public class UserServiceTest
{
    [Test]
    public void DeleteUser_Null()
    {
        IUserService sut = new UserService();
        var ex = Assert.Throws<ArgumentException>(() => sut.DeleteUser(null));
        Assert.True(ex.Message == "User must be valid.");
    }

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
    public void GetConnectionList_NullUser01()
    {
        IUserService sut = new UserService();

        var ex = Assert.Throws<ArgumentException>(() => sut.GetConnectionList(null, new User("Test"), 100));
        Assert.True(ex.Message == "Both users must be valid.");
    }

    [Test]
    public void GetConnectionList_NullUser02()
    {
        IUserService sut = new UserService();

        var ex = Assert.Throws<ArgumentException>(() => sut.GetConnectionList(new User("Test"), null, 100));
        Assert.True(ex.Message == "Both users must be valid.");
    }


    [Test]
    public void AddFriendship_User01Null()
    {
        IUserService sut = new UserService();
        var ex = Assert.Throws<ArgumentException>(() => sut.AddFriendship(null, new User("Test")));
        Assert.True(ex.Message == "Both users must be valid.");
    }

    [Test]
    public void AddFriendship_User02Null()
    {
        IUserService sut = new UserService();
        var ex = Assert.Throws<ArgumentException>(() => sut.AddFriendship(new User("Test"), null));
        Assert.True(ex.Message == "Both users must be valid.");
    }

    [Test]
    public void GetCommonFriend_User01Null()
    {
        IUserService sut = new UserService();
        var ex = Assert.Throws<ArgumentException>(() => sut.GetCommonFriends(null, new User("Test")));
        Assert.True(ex.Message == "Both users must be valid.");
    }

    [Test]
    public void GetCommonFriend_User02Null()
    {
        IUserService sut = new UserService();
        var ex = Assert.Throws<ArgumentException>(() => sut.GetCommonFriends(new User("Test"), null));
        Assert.True(ex.Message == "Both users must be valid.");
    }
}