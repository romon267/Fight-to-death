using System;
using System.Drawing;
using Console = Colorful.Console;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

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
            // Health, attack, block, potions.
            Warrior enemy = new Warrior("Bob Tabor", 150, 50, 25);
            Warrior player = new Warrior("Blewboar", 150, 50, 25);

            Console.Clear();
            Console.WriteLine(@"██████╗ ███████╗ █████╗ ████████╗██╗  ██╗    ███████╗██╗ ██████╗ ██╗  ██╗████████╗
██╔══██╗██╔════╝██╔══██╗╚══██╔══╝██║  ██║    ██╔════╝██║██╔════╝ ██║  ██║╚══██╔══╝
██║  ██║█████╗  ███████║   ██║   ███████║    █████╗  ██║██║  ███╗███████║   ██║   
██║  ██║██╔══╝  ██╔══██║   ██║   ██╔══██║    ██╔══╝  ██║██║   ██║██╔══██║   ██║   
██████╔╝███████╗██║  ██║   ██║   ██║  ██║    ██║     ██║╚██████╔╝██║  ██║   ██║   
╚═════╝ ╚══════╝╚═╝  ╚═╝   ╚═╝   ╚═╝  ╚═╝    ╚═╝     ╚═╝ ╚═════╝ ╚═╝  ╚═╝   ╚═╝   
                                                                                  ", Color.Red);
            Console.WriteLine("Press enter to start game or type \"exit\" to quit.", Color.AntiqueWhite);
            string answer = Console.ReadLine();
            if (answer == "exit")
            {
                return false;
            }
            else
            {
                Console.Clear();
                Battle.StartFight(player, enemy);
                return true;
            }
            
        }

        private static void CharacterCreation()
        {
            


            Console.WriteLine("Welcome to the arena, gladiator!", Color.AntiqueWhite);
            Thread.Sleep(1000);
            Console.WriteLine("Your name is Blewboar the almighty!", Color.AntiqueWhite);
            Thread.Sleep(1000);
            Console.WriteLine(@"Each turn you will have a chance to perform an action of some kind:\n
You may attack your opponent or use a healing potion, but be wise, you only have 3 of them on your belt!", Color.AntiqueWhite);
            Console.WriteLine("No go kill your opponent!", Color.AntiqueWhite);
            Console.ReadLine();
        }
    }
}
