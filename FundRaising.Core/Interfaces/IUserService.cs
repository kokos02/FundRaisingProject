using FundRaising.Core.Models;
using FundRaising.Core.Options;
using System.Collections.Generic;

namespace FundRaising.Core.Interfaces
{
    public interface IUserService
    {
        public Result<User> CreateUser(CreateUserOptions options);
        public Result<User> GetUserById(int userId);
        public Result<List<User>> GetAllUsers();
        public Result<bool> DeleteUser(int _userId);
        Result<bool> UpdateUser(int userId, UpdateUserOptions options);
    }
}



