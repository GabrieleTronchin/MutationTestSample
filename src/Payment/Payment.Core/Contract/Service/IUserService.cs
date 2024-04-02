using Payment.Core.Contract.Model;
using System.Collections.Generic;

namespace Payment.Core.Contract.Service
{
    public interface IUserService
    {
        /// <summary>
        /// Get a User by its Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IUser GetUser(string id);

        /// <summary>
        /// Create a User
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IUser CreateUser(string name);

        /// <summary>
        /// Add a friendship relation between user1 and user2 
        /// </summary>
        /// <param name="user1"></param>
        /// <param name="user2"></param>
        void AddFriendship(IUser user1, IUser user2);

        /// <summary>
        /// Delete a User
        /// </summary>
        /// <param name="user"></param>
        void DeleteUser(IUser user);

        /// <summary>
        /// Return the list of common friends of user1 and user2
        /// </summary>
        /// <param name="user1"></param>
        /// <param name="user2"></param>
        /// <returns></returns>
        IReadOnlyList<IUser> GetCommonFriends(IUser user1, IUser user2);

        /// <summary>
        /// This method returns the shortest list of users that connects two users by their mutual friends.
        /// If no list of mutual friends shorter or equal than maxLevel is found, it returns an empty list.
        /// If user1 and user2 are friends it returns the list [user1, user2]
        /// If user1 is friend with friend1 and friend1 is friend with user2, it returns [user1, friend1, user2].
        /// In general, if we note "-->" : is friend of and we have
        /// user1 --> friend 1 --> friend 2 --> ... --> friend N --> user2 , this method
        /// returns the list [user1, friend 1, friend 2, ..., friend N, user2]
        /// if N > maxLevel then the method return an empty list 
        /// </summary>
        /// <param name="user1"></param>
        /// <param name="user2"></param>
        /// <param name="maxLevel"></param>
        /// <returns></returns>
        IReadOnlyList<IUser> GetConnectionList(IUser user1, IUser user2, uint maxLevel);
    }
}