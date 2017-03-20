using OnlineMarket.DataAccessLayer.Entities;
using System.Collections.Generic;

namespace OnlineMarket.BusinessLogicLayer.Interfaces
{
    public interface IMembershipService
    {
        User CreateUser(string email, string password);

        IList<User> GetUsers();

        User GetUserByEmail(string email);

        bool IsUserAdmin(string email);

        void ChangeUserRole(int userId, int roleId);

        void RemoveUser(User user);

        MembershipContext ValidateUser(string username, string password);

        MembershipContext ConfirmEmail(string email, string code);

        void UpdateUserBalance(User user, double amount, bool add);
    }
}
