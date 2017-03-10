using OnlineMarket.DataAccessLayer.Entities;
using System.Collections.Generic;

namespace OnlineMarket.BusinessLogicLayer.Interfaces
{
    public interface IMembershipService
    {
        User CreateUser(string email, string password);

        IList<User> GetUsers();

        User GetUserByEmail(string email);

        void MoveUserToUnbannedGroup(User user);

        void MoveUserToBannedGroup(User user);

        void RemoveUser(User user);

        MembershipContext ValidateUser(string username, string password);
    }
}
