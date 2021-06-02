using FundRaising.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundRaising.Core.Options
{
    public class FundOptions
    {
        public int FundId { get; set; }
        public User User { get; set; }
        public Project Project { get; set; }
        
    }
}
