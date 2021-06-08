﻿using FundRaising.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundRaising.Core.Options
{
    public class SearchProjectOptions
    {
        public string text { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ProjectCategory? ProjectCategory { get; set; }
        public int? RewardId {get;set;}

    }
}
        


