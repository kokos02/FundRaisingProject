using FundRaising.Core.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundRaising.Core.Interfaces
{
    public interface IFundService
    {
        public bool FundProject(ProjectOptions _projectOptions, decimal _amount, UserOptions _userOptions);
    }
}
