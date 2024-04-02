using NUnit.Framework;
using Payment.Users;
using System;
using System.Linq;

namespace Payment.Test.Users;

[TestFixture]
public class UserMemoryRepositoryTest
{

    [Test]
    public void AddFriend_InvalidUser01()
    {
        var validUser = new User("Test");
        UserMemoryRepository.Save(validUser);
        var invalidUser = new User("InvalidUser");

        var ex = Assert.Throws<InvalidOperationException>(() => UserMemoryRepository.AddFriend(invalidUser.Id, validUser.Id));
        Assert.True(ex.Message.Contains($"System is not able to find any user with id={invalidUser.Id}"));
    }



    [Test]
    public void AddFriend_InvalidUser02()
    {
        var validUser = new User("Test");
        UserMemoryRepository.Save(validUser);
        var invalidUser = new User("InvalidUser");

        var ex = Assert.Throws<InvalidOperationException>(() => UserMemoryRepository.AddFriend(validUser.Id, invalidUser.Id));
        Assert.True(ex.Message.Contains($"System is not able to find any user with id={invalidUser.Id}"));
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
    [Test]
    public void FindUser_Valid()
    {
        var validUser = new User("Test");
        UserMemoryRepository.Save(validUser);


        var user = UserMemoryRepository.FindUser(validUser.Id);
        Assert.NotNull(user);
        Assert.AreEqual(user, validUser);
    }


    [Test]
    public void FindUser_null()
    {
        var invalidUser = new User("InvalidUser");
        var user = UserMemoryRepository.FindUser(invalidUser.Id);
        Assert.Null(user);
    }



}