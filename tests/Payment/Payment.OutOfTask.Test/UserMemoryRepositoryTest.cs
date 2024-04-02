using Payment.Users;

namespace Payment.OutOfTask.Test
{
    public class UserMemoryRepositoryTest
    {

        [Fact]
        public void AddFriend_InvalidUser01()
        {
            var validUser = new User("Test");
            UserMemoryRepository.Save(validUser);
            var invalidUser = new User("InvalidUser");

            var ex = Assert.Throws<InvalidOperationException>(() => UserMemoryRepository.AddFriend(invalidUser.Id, validUser.Id));
            Assert.Contains($"System is not able to find any user with id={invalidUser.Id}", ex.Message);
        }



        [Fact]
        public void AddFriend_InvalidUser02()
        {
            var validUser = new User("Test");
            UserMemoryRepository.Save(validUser);
            var invalidUser = new User("InvalidUser");

            var ex = Assert.Throws<InvalidOperationException>(() => UserMemoryRepository.AddFriend(validUser.Id, invalidUser.Id));
            Assert.Contains($"System is not able to find any user with id={invalidUser.Id}", ex.Message);
        }

        [Fact]
        public void GetCommonFriend_InvalidUser01()
        {
            var validUser = new User("Test");
            UserMemoryRepository.Save(validUser);
            var invalidUser = new User("InvalidUser");

            var ex = Assert.Throws<InvalidOperationException>(() => UserMemoryRepository.GetCommonFriends(invalidUser.Id, validUser.Id));
            Assert.Contains($"System is not able to find any user with id={invalidUser.Id}", ex.Message);

        }

        [Fact]
        public void GetCommonFriend_InvalidUser02()
        {
            var validUser = new User("Test");
            UserMemoryRepository.Save(validUser);
            var invalidUser = new User("InvalidUser");

            var ex = Assert.Throws<InvalidOperationException>(() => UserMemoryRepository.GetCommonFriends(validUser.Id, invalidUser.Id));
            Assert.Contains($"System is not able to find any user with id={invalidUser.Id}", ex.Message);

        }
        [Fact]
        public void FindUser_Valid()
        {
            var validUser = new User("Test");
            UserMemoryRepository.Save(validUser);


            var user = UserMemoryRepository.FindUser(validUser.Id);
            Assert.NotNull(user);
            Assert.Equal(user, validUser);
        }


        [Fact]
        public void FindUser_null()
        {
            var invalidUser = new User("InvalidUser");
            var user = UserMemoryRepository.FindUser(invalidUser.Id);
            Assert.Null(user);
        }



    }
}