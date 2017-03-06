using OnlineMarket.DataAccessLayer.Entities;
using System.Collections.Generic;

namespace OnlineMarket.BusinessLogicLayer.Interfaces
{
    public interface IUserService
    {
        IList<User> GetUsers();

        void AddUser(User user);

        void MoveUserToUnbannedGroup(User user);

        void MoveUserToBannedGroup(User user);

        void RemoveUser(User user);
    }
}
