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

                // I don't yet know how to implement character creation properly in other function, it creates only local variables.
                // Health, attack, block, potions.
                Player player = new Player("Blewboar", 150);
                Enemy enemy = new Enemy("Bob Tabor", 150);
                player.SetWeapon("wooden dagers", "Daggers", 55, 0);
                player.EquipWeapon();
                enemy.SetWeapon("Bob's faithful shield", "Shield", 50, 25);
                enemy.EquipWeapon();
                Console.WriteLine("Welcome to the arena, gladiator!", Color.AntiqueWhite);
                Thread.Sleep(1000);
                Console.WriteLine($"Your name is {player.Name} the almighty!", Color.AntiqueWhite);
                Thread.Sleep(1000);
                player.GetWeapon();
                enemy.GetWeapon();

                /*
                Console.WriteLine(@"Each turn you will have a chance to perform an action of some kind:\n
    You may attack your opponent or use a healing potion, but be wise, you only have 3 of them on your belt!", Color.AntiqueWhite);
                */
                Console.WriteLine("No go kill your opponent!", Color.AntiqueWhite);
                Console.ReadLine();

                Console.Clear();
                Battle.StartFight(player, enemy);
                return true;
            }
            
        }
    }
}
