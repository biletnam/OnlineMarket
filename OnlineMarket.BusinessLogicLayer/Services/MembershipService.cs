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
            var passwordSalt = _encryptionService.CreateSalt();
            var user = new User
            {
                Email = email,
                Password = _encryptionService.EncryptPassword(password, passwordSalt),
                Salt = passwordSalt,
                IsConfirmEmail = false,
                ConfirmCode = Guid.NewGuid().ToString(),
                Balance = 0,
                RoleId = (int) Roles.User
            };
            _unitOfWork.UserRepository.Add(user);
            _unitOfWork.SaveChanges();

            return user;
        }

        public IList<User> GetUsers()
        {
            return _unitOfWork.UserRepository.GetAll();
        }

        public User GetUserByEmail(string email)
        {
            return _unitOfWork.UserRepository.Find(u => u.Email == email).FirstOrDefault();
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
            var user = GetUserByEmail(email);

            if (user == null || !IsUserValid(user, password)) return null;

            return CreateMembershipContext(user);
        }

        public MembershipContext ConfirmEmail(string email, string code)
        {
            var user = GetUserByEmail(email);

            if (user == null) return null;

            user.IsConfirmEmail = true;
            _unitOfWork.UserRepository.Update(user);
            _unitOfWork.SaveChanges();

            return CreateMembershipContext(user);
        }

        private MembershipContext CreateMembershipContext(User user)
        {
            var membershipContext = new MembershipContext {User = user};
            var identity = new GenericIdentity(user.Email);
            membershipContext.Principal = new GenericPrincipal(identity, new[] { user.Role.Title });

            return membershipContext;
        }

        private bool IsPasswordValid(User user, string password)
        {
            return string.Equals(_encryptionService.EncryptPassword(password, user.Salt), user.Password);
        }
  
        private bool IsEmailConfirmed(string email)
        {
            return GetUserByEmail(email).IsConfirmEmail;
        }

        private bool IsUserValid(User user, string password)
        {
            return IsPasswordValid(user, password) && IsEmailConfirmed(user.Email) && user.RoleId != (int)Roles.Banned;
        }
    }
}
