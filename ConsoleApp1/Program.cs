using FundRaising.Core.Data;
using FundRaising.Core.Interfaces;
using FundRaising.Core.Models;
using FundRaising.Core.Options;
using FundRaising.Core.Services;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            FundRaisingDbContext db = new();

            UserOptions aa = new()
            {
                Username = "aaaa"
            };

            ProjectOptions bb = new()
            {
                Title = "bbb"
            };

            IProjectService newProject = new ProjectService(db);

            
        }
    }
}
