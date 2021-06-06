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
            
            
            Console.WriteLine("Hello World!");
            var user = new User();
            var project1 = new Project();
            Console.WriteLine(project1.Created);

            var projectOptions = new ProjectOptions()
            {
                Title = "titlos",
                Description = "perigrafh"
            };

            IProjectService service = new ProjectService(db);


            
        }
    }
}
