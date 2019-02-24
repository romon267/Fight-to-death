using System;
using System.Drawing;
using Console = Colorful.Console;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightToDeath
{
    class Program
    {
        static void Main(string[] args)
        {
            

            bool displayMenu = true;
            while (displayMenu)
            {
                MainMenu();
            }
            
            Console.ReadLine();

        }

        private static bool MainMenu()
        {
            Warrior bob = new Warrior("Bob Tabor", 100, 100, 50);
            Warrior blewboar = new Warrior("Blewboar", 100, 100, 50);

            Console.Clear();
            Console.WriteLine("DEATH FIGHT");
            Console.WriteLine("Press enter to start game or type \"exit\" to quit.");
            string answer = Console.ReadLine();
            if (answer == "exit")
            {
                return false;
            }
            else
            {
                Battle.StartFight(bob, blewboar);
                return true;
            }
            
        }
    }
}
