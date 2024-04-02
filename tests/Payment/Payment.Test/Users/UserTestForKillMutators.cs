using NUnit.Framework;
using Payment.Users;
using System;

namespace Payment.Test.Users;

[TestFixture]
public class UserTestForKillMutators
{
    [Test]
    public void Initialization_Correct()
    {
        var user = new User("Test");
        Assert.False(user.Id.Contains("-"));
        Assert.True(Guid.TryParse(user.Id, out var _));
    }

    [Test]
    public void DeleteUser_Null()
    {
        IUserService sut = new UserService();
        var ex = Assert.Throws<ArgumentException>(() => sut.DeleteUser(null));
        Assert.True(ex.Message == "User must be valid.");
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

    [Test]
    public void GetCommonFriend_InvalidUser01()
    {
        var validUser = new User("Test");
        UserMemoryRepository.Save(validUser);
        var invalidUser = new User("InvalidUser");

        var ex = Assert.Throws<InvalidOperationException>(() => UserMemoryRepository.GetCommonFriends(invalidUser.Id, validUser.Id));
        Assert.True(ex.Message.Contains($"System is not able to find any user with id={invalidUser.Id}"));

    }

    [Test]
    public void GetCommonFriend_InvalidUser02()
    {
        var validUser = new User("Test");
        UserMemoryRepository.Save(validUser);
        var invalidUser = new User("InvalidUser");

        var ex = Assert.Throws<InvalidOperationException>(() => UserMemoryRepository.GetCommonFriends(validUser.Id, invalidUser.Id));
        Assert.True(ex.Message.Contains($"System is not able to find any user with id={invalidUser.Id}"));

    }
}