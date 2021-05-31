using FundRaising.Core.Data;
using FundRaising.Core.Interfaces;
using FundRaising.Core.Models;
using FundRaising.Core.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundRaising.Core.Services
{
    public class UserService : IUserService
    {
        private readonly FundRaisingDbContext _dbContext;
        public UserService(FundRaisingDbContext _db)
        {
            _dbContext = _db;
        }
        public UserOptions CreateUser(UserOptions _userOptions)
        {
            User _newUser = new()
            {
                Username = _userOptions.Username,

                Email = _userOptions.Email
            };

            _dbContext.Users.Add(_newUser);

            _dbContext.SaveChanges();

            return new UserOptions
            {
                UserId = _newUser.UserId,

                Username = _newUser.Username,

                Email = _newUser.Email
            };

        }


        public List<UserOptions> GetAllUsers()
        {
            List<User> _users = _dbContext.Users.ToList();

            List<UserOptions> _userOptions = new();

            _users.ForEach(user => _userOptions.Add(new UserOptions()
            {
                UserId = user.UserId,

                Username = user.Username,

                Email = user.Email

            }));


            return _userOptions;
        }

        public UserOptions GetUserById(int _userId)
        {
            User _user = _dbContext.Users.Find(_userId);

            UserOptions _userOptions = new()
            {
                UserId = _user.UserId,

                Username = _user.Username,

                Email = _user.Email

            };

            return _userOptions;

        }

        public bool UpdateUser(int _userId, UserOptions _userOptions)
        {
            if (_userOptions == null) return false;

            User _user = _dbContext.Users.FirstOrDefault(user => user.UserId == _userId);

            _user.Username = _userOptions.Username;

            _user.Email = _userOptions.Email;

            _dbContext.SaveChanges();

            return true;

        }

        public bool DeleteUser(int _userId)
        {
            User _user = _dbContext.Users.Find(_userId);

            if (_user == null) return false;

            _dbContext.Users.Remove(_user);

            return true;

        }
    }
}

