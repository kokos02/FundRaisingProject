using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundRaising.Core.Options
{
    public class UpdateProjectOptions
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ProjectCategory { get; set; }
        public decimal TargetFund { get; set; }
        public DateTime Deadline { get; set; }
    }
}
