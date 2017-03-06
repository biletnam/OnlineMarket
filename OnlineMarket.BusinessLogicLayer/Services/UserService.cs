using OnlineMarket.BusinessLogicLayer.Interfaces;
using OnlineMarket.DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineMarket.DataAccessLayer.Entities;

namespace OnlineMarket.BusinessLogicLayer.Services
{
    public class UserService : IUserService
    {
        private IUnitOfWork _unitOfWork;

        private enum Roles { Administrator = 1, User, Banned }

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddUser(User user)
        {
            user.RoleId = (int)Roles.User;
            _unitOfWork.UserRepository.Add(user);
            _unitOfWork.SaveChanges();
        }

        public IList<User> GetUsers()
        {
            return _unitOfWork.UserRepository.GetAll();
        }

        public void MoveUserToBannedGroup(User user)
        {
            user.RoleId = (int)Roles.Banned;
            _unitOfWork.UserRepository.Update(user);
            _unitOfWork.SaveChanges();
        }

        public void RemoveUser(User user)
        {
            _unitOfWork.UserRepository.Remove(user);
            _unitOfWork.SaveChanges();
        }
    }
}
