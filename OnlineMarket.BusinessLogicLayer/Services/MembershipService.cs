using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using OnlineMarket.BusinessLogicLayer.Interfaces;
using OnlineMarket.DataAccessLayer.Entities;
using OnlineMarket.DataAccessLayer.Interfaces;
using OnlineMarket.Utilities.Interfaces;

namespace OnlineMarket.BusinessLogicLayer.Services
{
    public class MembershipService : IMembershipService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IEncryptionService _encryptionService;

        private enum Roles
        {
            Administrator = 1,
            User,
            Banned
        }

        public MembershipService(IUnitOfWork unitOfWork, IEncryptionService encryptionService)
        {
            _unitOfWork = unitOfWork;
            _encryptionService = encryptionService;
        }

        public User CreateUser(string email, string password)
        {
            var existingUser = _unitOfWork.UserRepository.Find(u => u.Email == email);

            if (existingUser == null) throw new Exception("Username is already in use");

            var passwordSalt = _encryptionService.CreateSalt();
            var user = new User
            {
                Email = email,
                Password = _encryptionService.EncryptPassword(password, passwordSalt),
                Salt = passwordSalt,
                Balance = 0,
                RoleId = (int) Roles.User
            };
            _unitOfWork.UserRepository.Add(user);
            AddUserResources(user);
            _unitOfWork.SaveChanges();

            return user;
        }

        public IList<User> GetUsers()
        {
            return _unitOfWork.UserRepository.GetAll();
        }

        public User GetUserByEmail(string email)
        {
            return _unitOfWork.UserRepository.Find(u => u.Email == email).First();
        }

        public bool IsUserAdmin(string email)
        {
            return GetUserByEmail(email).RoleId == (int) Roles.Administrator;
        }

        public void ChangeUserRole(int userId, int roleId)
        {
            var existingUser = _unitOfWork.UserRepository.Find(u => u.Id == userId).First();
            existingUser.RoleId = roleId;
            _unitOfWork.UserRepository.Update(existingUser);
            _unitOfWork.SaveChanges();
        }

        public void RemoveUser(User user)
        {
            _unitOfWork.UserRepository.Remove(user);
            _unitOfWork.SaveChanges();
        }

        public MembershipContext ValidateUser(string email, string password)
        {
            var membershipContext = new MembershipContext();

            var userList = _unitOfWork.UserRepository.Find(u => u.Email == email);

            if (userList.Count == 0 || !IsUserValid(userList.First(), password)) return membershipContext;

            var user = userList.First();
            membershipContext.User = user;
            var identity = new GenericIdentity(user.Email);
            membershipContext.Principal = new GenericPrincipal(identity, new[] {user.Role.Title});

            return membershipContext;
        }

        private bool IsPasswordValid(User user, string password)
        {
            return string.Equals(_encryptionService.EncryptPassword(password, user.Salt), user.Password);
        }

        private bool IsUserValid(User user, string password)
        {
            return IsPasswordValid(user, password) && user.RoleId != (int) Roles.Banned;
        }

        private void AddUserResources(User user)
        {
            foreach (var resource in _unitOfWork.ResourceRepository.GetAll())
            {
                _unitOfWork.UserResourcesRepository.Add(new UserResources
                {
                    UserId = user.Id,
                    ResourceId = resource.Id,
                    Quantity = 0
                });
            }
        }

        public void UpdateUserBalance(User user, double amount, bool add)
        {
            user.Balance = add ? user.Balance + amount : user.Balance - amount;
            _unitOfWork.UserRepository.Update(user);
            _unitOfWork.SaveChanges();
        }
    }
}
