using FundRaising.Core.Models;
using FundRaising.Core.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundRaising.Core.Interfaces
{
    public interface IUserService
    {
        public Result<User> CreateUser(UserOptions _userOptions);
        public Result<List<User>> GetAllUsers();
        public Result<User> GetUserById(int _userId);
        //public bool UpdateUser(int _userId, UserOptions _userOptions);
        public Result<int> DeleteUser(int _userId);
        
    }
}
