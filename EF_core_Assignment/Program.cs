using EF_core_Assignment.Data;
using EF_core_Assignment.Models;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EF_core_Assignment
{
    class Program
    {
        public static bool running = true;

        static void Main(string[] args)
        {
            Console.WriteLine("Trying to seed database...");
            using (var context = new AppDbContext())
            {
                Seeding.SeedDatabase(context);
            }

            Console.WriteLine("Press B for browsing database and A to add to database: ");
            var consoleKeyInfo1 = Console.ReadKey().KeyChar;

            switch (char.ToUpper(consoleKeyInfo1))
            {
                case 'B':
                        
                    View.Browse.Execute();
                    break;
                case 'A':
                    View.Add.Execute();
                    break;
            }           
        }
    }    
}



