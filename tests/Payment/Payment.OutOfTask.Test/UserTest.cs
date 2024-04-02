using Payment.Users;

namespace Payment.OutOfTask.Test
{
    public class UserTest
    {
        [Fact]
        public void Initialization_Correct()
        {
            var user = new User("Test");
            Assert.DoesNotContain("-", user.Id);
            Assert.True(Guid.TryParse(user.Id, out var _));
        }
    }
}