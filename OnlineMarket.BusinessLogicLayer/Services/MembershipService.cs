using System;
using System.Collections.Generic;
using OnlineMarket.BusinessLogicLayer.Interfaces;
using OnlineMarket.DataAccessLayer.Entities;
using OnlineMarket.DataAccessLayer.Interfaces;
using System.Linq;
using System.Security.Principal;

namespace OnlineMarket.BusinessLogicLayer.Services
{
    public class MembershipService : IMembershipService
    {
        private IUnitOfWork _unitOfWork;

        private IEncryptionService _encryptionService;

        private enum Roles { Administrator = 1, User, Banned }

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
                RoleId = (int)Roles.User
            };
            _unitOfWork.UserRepository.Add(user);
            _unitOfWork.SaveChanges();

            return user;
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

        public void MoveUserToUnbannedGroup(User user)
        {
            user.RoleId = (int)Roles.User;
            _unitOfWork.UserRepository.Update(user);
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
            var user = _unitOfWork.UserRepository.Find(u => u.Email == email).First();

            if (user != null && isUserValid(user, password))
            {
                membershipContext.User = user;
                var identity = new GenericIdentity(user.Email);
                membershipContext.Principal = new GenericPrincipal(identity, new []{ user.Role.Title });
            }

            return membershipContext;
        }

        private bool isPasswordValid(User user, string password)
        {
            return string.Equals(_encryptionService.EncryptPassword(password, user.Salt), user.Password);
        }

        private bool isUserValid(User user, string password)
        {
            return isPasswordValid(user, password) ? !(user.RoleId == (int)Roles.Banned) : false;
        }
    }
}
