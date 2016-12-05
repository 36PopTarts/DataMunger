using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DataMunger
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                Weatherman weatherData = new Weatherman(Path.Combine(Directory.GetCurrentDirectory(), args[0]));
                FootballManager footballData = new FootballManager(Path.Combine(Directory.GetCurrentDirectory(), args[1]));                
                Console.WriteLine("Day with minimum temperature spread: " + weatherData.findDayWithMinTemperatureDiff());
                Console.WriteLine("Team with minimum goal spread: " + footballData.findTeamWithMinGoalDiff());
            }
            else            
                Console.WriteLine("Please specify at least one filename.");
            
            Console.ReadKey();
        }
    }
}
