﻿using FundRaising.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundRaising.Core.Options
{
    public class CreateProjectOptions
    {
        public int CreatorId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ProjectCategory { get; set; }
        public DateTime Deadline { get; set; }
        public decimal TargetFund { get; set; }
    }
}