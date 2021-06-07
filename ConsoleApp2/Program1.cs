using FundRaising.Core.Data;
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


            //var newproject = new CreateProjectOptions
            //{
            //    CreatorId = 1,
            //    TargetFund = 28383.456m,
            //    Deadline = DateTime.Parse("2022/8/8"),

            //};

            //var user = new User();
            //var project = new Project();
            //project = projectservice.GetProjectById(1).Data;

            //userservice.CreateUser(newuser);
            //projectservice.CreateProject(newproject);

            //var reward1 = new Reward();
            //var reward2 = new Reward();

            //project.AvailableRewards.Add(reward1);
            //project.AvailableRewards.Add(reward2);

            //var user18 = new CreateUserOptions();
            //userservice.CreateUser(user18);
            //var user25 = new CreateUserOptions
            //{
            //    Username = "john",
            //    Email = "ats",
            //    Password = "agdhd"
            //};

            //userservice.CreateUser(user25);
            var user100 = new User();
            var project100 = new Project();
            var reward100 = new Reward();
            var userreward100 = new UserReward();

            var project300 = new CreateProjectOptions();
            projectservice.CreateProject(project300);

            var project400 = new CreateProjectOptions
            {
                   CreatorId = 10,
                   ProjectCategory = "7"
            };

            projectservice.CreateProject(project400);




        }
    }

}

                





