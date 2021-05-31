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
        public UserOptions CreateUser(UserOptions _userOptions);
        public List<UserOptions> GetAllUsers();
        public UserOptions GetUserById(int _userId);
        public bool UpdateUser(int _userId, UserOptions _userOptions);
        public bool DeleteUser(int _userId);
    }
}
