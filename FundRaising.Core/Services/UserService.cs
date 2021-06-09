using FundRaising.Core.Data;
using FundRaising.Core.Interfaces;
using FundRaising.Core.Models;
using FundRaising.Core.Options;
using System.Collections.Generic;
using System.Linq;

namespace FundRaising.Core.Services
{
    public class UserService : IUserService
    {
        private readonly FundRaisingDbContext db;
        public UserService(FundRaisingDbContext _db)
        {
            db = _db;
        }
        public Result<User> CreateUser(CreateUserOptions options)
        {
            if (options == null)
            {
                return Result<User>.ServiceFailed(StatusCode.BadRequest, "Null Options");
            }

            var user = new User()
            {
                Username = options.Username,
                Email = options.Email,
                Password = options.Password
            };

            db.Add(user);

            if (db.SaveChanges() <= 0)
            {
                return Result<User>.ServiceFailed(StatusCode.InternalServerError, "User could not be created");
            }
            return Result<User>.ServiceSuccessful(user);
        }

        public Result<User> GetUserById(int userId)
        {
            var user = db.Users.Find(userId);
            //var user = SearchUser(new SearchProjectOptions
            //{
            //    RewardId = rewardId
            //}).SingleOrDefault();
            if (user == null)
            {
                return Result<User>.ServiceFailed(StatusCode.NotFound, $"There is no user with this Id: {userId}");
            }
            return Result<User>.ServiceSuccessful(user);
        }

        public Result<List<User>> GetAllUsers()
        {
            List<User> users = db.Users.ToList();

            return new Result<List<User>>
            {
                Data = users
            };
        }

        public Result<bool> DeleteUser(int userId)
        {
            var user = GetUserById(userId).Data;
            if (user == null)
            {
                return Result<bool>.ServiceFailed(StatusCode.BadRequest, $"User with {userId} was not found");
            }

            db.Users.Remove(user);

            if (db.SaveChanges() <= 0)
            {
                return Result<bool>.ServiceFailed(StatusCode.InternalServerError, "User could not be deleted");
            }
            return Result<bool>.ServiceSuccessful(true);
        }

        public Result<bool> UpdateUser(int userId, UpdateUserOptions options)
        {
            if (options == null)
            {
                return Result<bool>.ServiceFailed(StatusCode.BadRequest, "Null options");
            }

            var user = GetUserById(userId).Data;

            if (user == null)
            {
                return Result<bool>.ServiceFailed(StatusCode.BadRequest, $"User with {userId} was not found");
            }
            
            if (user.Username != options.Username)
            {
                user.Username = options.Username;
            }

            if (user.Email != options.Email)
            {
                user.Email = options.Email;
            }

            if (user.Password != options.Password)
            {
                user.Password = options.Password;
            }

            if (db.SaveChanges() <= 0)
            {
                return Result<bool>.ServiceFailed(StatusCode.InternalServerError, "User could not be updated");
            }

            return Result<bool>.ServiceSuccessful(true);
        }
    }
}
        






