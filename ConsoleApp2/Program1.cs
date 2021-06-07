﻿using FundRaising.Core.Data;
using FundRaising.Core.Interfaces;
using FundRaising.Core.Models;
using FundRaising.Core.Options;
using FundRaising.Core.Services;
using Microsoft.EntityFrameworkCore;
using System;

namespace ConsoleApp2
{
    public class Program1
    {

        public static void Main(string[] args)
        {

            using FundRaisingDbContext db = new();
            IUserService userservice = new UserService(db);
            IProjectService projectservice = new ProjectService(db, userservice);
            IRewardService rewardservice = new RewardService(db, projectservice);
            IUserRewardService userrewardservice = new UserRewardService(db, userservice, projectservice, rewardservice);

            //var newuser = new CreateUserOptions
            //{
            //    Username = "nikos",
            //    Email = "nikos@gmail.com",
            //    Password = "12345",

            //};

            //var user = new User();
            //var newproject = new Project
            //{
            //    CreatorId = 1,
            //    TargetFund = 28383.456m,
            //    Deadline = DateTime.Parse("2022/8/8"),

            //};

            //userservice.CreateUser(newuser);
            //projectservice.CreateProject(newproject);

            //var reward1 = new Reward(); 
            //var reward2 = new Reward();

            //newproject.AvailableRewards.Add(reward1);
            //newproject.AvailableRewards.Add(reward2);





        }
    }

}

                





