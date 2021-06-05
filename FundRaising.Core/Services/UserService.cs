using FundRaising.Core.Data;
using FundRaising.Core.Interfaces;
using FundRaising.Core.Models;
using FundRaising.Core.Options;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundRaising.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IFundRaisingDbContext _dbContext;
        private readonly ILogger<UserService> _logger;
        public UserService(IFundRaisingDbContext db)
        {
            _dbContext = db;
        }
        public Result<User> CreateUser(UserOptions _userOptions)
        {
            if (_userOptions == null)
            {
                return new Result<User>(ErrorCode.BadRequest, "Null options.");
            }

            if (string.IsNullOrWhiteSpace(_userOptions.Username) ||
              string.IsNullOrWhiteSpace(_userOptions.Email) ||
              string.IsNullOrWhiteSpace(_userOptions.Password))
            {
                return new Result<User>(ErrorCode.BadRequest, "Not all required user options provided.");
            }

            var _userWithSameUsername = _dbContext.Users.SingleOrDefault(user => user.Username == _userOptions.Username);

            if (_userWithSameUsername != null)
            {
                return new Result<User>(ErrorCode.Conflict, $"Customer with #{_userOptions.Username} already exists.");
            }

            var _newUser = new User
            {
                Username = _userOptions.Username,
                Email = _userOptions.Email,
                Password = _userOptions.Password
            };

             _dbContext.Users.Add(_newUser);

            try
            {
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return new Result<User>(ErrorCode.InternalServerError, "Could not save user.");
            }

            return new Result<User>
            {
                Data = _newUser
            };
        }



        public Result<List<User>> GetAllUsers()
        {
            // List<User> _users = _dbContext.Users.ToList();

            // List<UserOptions> _userOptions = new();

            // _users.ForEach(user => _userOptions.Add(new UserOptions()
            //{
            //   UserId = user.UserId,

            //  Username = user.Username,

            // Email = user.Email

            //}));

            var users =  _dbContext.Users.ToList();

            return new Result<List<User>>
            {
                Data = users.Count > 0 ? users : new List<User>()
            };

        }

        public Result<User> GetUserById(int _userId)
        {
            //User _user = _dbContext.Users.Find(_userId);

            //UserOptions _userOptions = new()
            //{
            // UserId = _user.UserId,

            // Username = _user.Username,

            //Email = _user.Email

            //};

            //return _userOptions;
            if (_userId <= 0)
            {
                return new Result<User>(ErrorCode.BadRequest, "Id cannot be less than or equal to zero.");
            }

            var user =  _dbContext
                .Users
                .SingleOrDefault(user => user.UserId == _userId);

            if (user == null)
            {
                return new Result<User>(ErrorCode.NotFound, $"Customer with id #{_userId} not found.");
            }

            return new Result<User>
            {
                Data = user
            };

        }
        //UpdateUser
        //public bool UpdateUser(int _userId, UserOptions _userOptions)
        //{
         //   if (_userOptions == null) return false;

          //  User _user = _dbContext.Users.FirstOrDefault(user => user.UserId == _userId);

          //  _user.Username = _userOptions.Username;

          //  _user.Email = _userOptions.Email;

          //  _dbContext.SaveChanges();

         //   return true;

       // }

        public Result<int> DeleteUser(int _userId)
        {
            //User _user = _dbContext.Users.Find(_userId);

            //if (_user == null) return false;

            //_dbContext.Users.Remove(_user);

            //return true;
            var userToDelete = GetUserById(_userId);

            if (userToDelete.Error != null || userToDelete.Data == null)
            {
                return new Result<int>(ErrorCode.NotFound, $"User with id #{_userId} not found.");
            }

            _dbContext.Users.Remove(userToDelete.Data);

            try
            {
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return new Result<int>(ErrorCode.InternalServerError, "Could not delete customer.");
            }

            return new Result<int>
            {
                Data = _userId
            };

        }
    }
}

