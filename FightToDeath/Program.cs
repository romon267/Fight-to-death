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

        static Player player = new Player();
        static Enemy enemy1 = new Enemy("Bob Tabor", 150, 0, 0, 120);

        public static bool playerDead = false;

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
                GameLoop(player, enemy1);
                Console.ReadLine();
                return true;
            } 
        }

        public static void GameLoop(Player player, Enemy enemy)
        {
            CharacterCreation(player, enemy);
            Console.Clear();
            while (playerDead == false)
            {
                EnterNewLevel();
                Battle.StartFight(player, enemy);
                if (playerDead == true)
                {
                    break;
                }
                else
                {
                    Shop(player);
                }
            }
            Console.WriteLine(@"
  ▄████  ▄▄▄       ███▄ ▄███▓▓█████     ▒█████   ██▒   █▓▓█████  ██▀███  
 ██▒ ▀█▒▒████▄    ▓██▒▀█▀ ██▒▓█   ▀    ▒██▒  ██▒▓██░   █▒▓█   ▀ ▓██ ▒ ██▒
▒██░▄▄▄░▒██  ▀█▄  ▓██    ▓██░▒███      ▒██░  ██▒ ▓██  █▒░▒███   ▓██ ░▄█ ▒
░▓█  ██▓░██▄▄▄▄██ ▒██    ▒██ ▒▓█  ▄    ▒██   ██░  ▒██ █░░▒▓█  ▄ ▒██▀▀█▄  
░▒▓███▀▒ ▓█   ▓██▒▒██▒   ░██▒░▒████▒   ░ ████▓▒░   ▒▀█░  ░▒████▒░██▓ ▒██▒
 ░▒   ▒  ▒▒   ▓▒█░░ ▒░   ░  ░░░ ▒░ ░   ░ ▒░▒░▒░    ░ ▐░  ░░ ▒░ ░░ ▒▓ ░▒▓░
  ░   ░   ▒   ▒▒ ░░  ░      ░ ░ ░  ░     ░ ▒ ▒░    ░ ░░   ░ ░  ░  ░▒ ░ ▒░
░ ░   ░   ░   ▒   ░      ░      ░      ░ ░ ░ ▒       ░░     ░     ░░   ░ 
      ░       ░  ░       ░      ░  ░       ░ ░        ░     ░  ░   ░     
                                                     ░                   
", Color.Red);
            Console.WriteLine($"You killed {Battle.enemiesKilled} opponent(s)!");
            Battle.ResetChar(player);
            Console.ReadLine();
        }

        public static void EnterNewLevel()
        {
            Console.WriteLine($"Level: {Battle.enemiesKilled + 1}");
            Console.WriteLine("You enter the arena, you see your enemy and the crowd watching.", Color.GhostWhite);
            Console.WriteLine("You approach... Press enter to start fight!", Color.GhostWhite);
            Console.ReadLine();
        }

        public static void CharacterCreation(Player player, Enemy enemy)
        {
            Console.WriteLine("Welcome to the arena, gladiator!", Color.AntiqueWhite);
            Console.WriteLine("What is your name?", Color.AntiqueWhite);
            string playerName;
            do
            {
                playerName = Console.ReadLine();
                if (playerName == "" || playerName == " ")
                {
                    Console.WriteLine("Please input any name");
                }
            } while (playerName != "" ^ playerName != " ");
            
            player.Name = playerName;
            Console.WriteLine($"Greetings to you, {player.Name}!", Color.AntiqueWhite);
            Console.WriteLine("I can't tell, what race are you? Elf or dwarf?", Color.AntiqueWhite);
            string playerRace;
            do
            {
                playerRace = Console.ReadLine();
                if (playerRace.ToLower() == "elf")
                {
                    Console.WriteLine("*Some elvish words of greetings, somehow you cant understand*", Color.AntiqueWhite);
                    player.Health = 120;
                    player.BaseAttack += 15;
                }
                else if (playerRace.ToLower() == "dwarf")
                {
                    player.Health += 170;
                    player.BaseBlock += 10;
                    Console.WriteLine("Mighty son of stone, indeed!", Color.AntiqueWhite);
                }
                else
                {
                    Console.WriteLine("Choose any race: \"elf\"or \"dwarf\"");
                }
            } while (playerRace != "elf" && playerRace != "dwarf");
            string playerWep;
            do
            {


                Console.WriteLine("What weapon you are using? Shield or daggers?");
                playerWep = Console.ReadLine();
                if (playerWep.ToLower() == "shield")
                {
                    player.SetWeapon(shield1);
                    player.EquipWeapon();
                    Console.WriteLine("You got a robust guard!");
                }
                else if (playerWep.ToLower() == "daggers")
                {
                    player.SetWeapon(dagger1);
                    player.EquipWeapon();
                    Console.WriteLine("Tear your opponent with your crits!");
                }
                else
                {
                    Console.WriteLine("Please answer properly.");
                }
            } while (playerWep != "shield" && playerWep != "daggers");
            Console.WriteLine("-------");
            Console.WriteLine(@"Each turn you will have a chance to perform an action of some kind:
You may attack your opponent or use a healing potion, or perform special action according to
your class! Press info every turn to check your and enemy's health and check who have specials currently!", Color.AntiqueWhite);
            Console.WriteLine($"{player.Name}, you are {playerRace} and wielding a {player.Weapon.Name}, good luck!");
            Console.WriteLine("-------");
            Console.WriteLine("Your first opponent:");
            enemy.SetWeapon(shield1);
            enemy.EquipWeapon();
            Console.WriteLine($"{enemy.Name} wielding {enemy.Weapon.Name}.");
            Console.ReadLine();
        }
        
        public static void Shop(Player player)
        {
            string choice;
            do
            {

                Console.WriteLine("$$$$$$$", Color.Gold);
                Console.WriteLine($"Welcome to the store, champion, what do you want to buy?", Color.Gold);
                Console.WriteLine($"Your gold: {player.Gold}", Color.Gold);
                Console.WriteLine("1. Weapons\n2. Armor\n3. Potions\n4. Heal for 10 golds\n5. Leave", Color.Gold);
                choice = Console.ReadLine();
                Console.WriteLine("-------", Color.GhostWhite);
                if (choice == "1")
                {
                    Console.WriteLine($"1.{shield2.Name}: {shield2.Cost} gold.\n2.{shield3.Name}: {shield3.Cost} gold.", Color.AliceBlue);
                    Console.WriteLine($"3.{shield4.Name}: {shield4.Cost} gold.\n4.{shield5.Name}: {shield5.Cost} gold.", Color.AliceBlue);
                    Console.WriteLine("-------");
                    Console.WriteLine($"5.{dagger2.Name}: {dagger2.Cost} gold.\n6.{dagger3.Name}: {dagger3.Cost} gold.", Color.BlueViolet);
                    Console.WriteLine($"7.{dagger4.Name}: {dagger4.Cost} gold.\n8.{dagger5.Name}: {dagger5.Cost} gold.", Color.BlueViolet);

                    string buy = Console.ReadLine();
                    if (buy == "1")
                    {
                        Trade(player, shield2);
                    }
                    else if (buy == "2")
                    {
                        Trade(player, shield3);
                    }
                    else if (buy == "3")
                    {
                        Trade(player, shield4);
                    }
                    else if (buy == "4")
                    {
                        Trade(player, shield5);
                    }
                    else if (buy == "5")
                    {
                        Trade(player, dagger2);
                    }
                    else if (buy == "6")
                    {
                        Trade(player, dagger3);
                    }
                    else if (buy == "7")
                    {
                        Trade(player, dagger4);
                    }
                    else if (buy == "8")
                    {
                        Trade(player, dagger5);
                    }
                }
                else if (choice == "2")
                {
                    Console.WriteLine("No armor loool, just spam overheal, dude.", Color.OrangeRed);
                }
                else if (choice == "3")
                {
                    Trade(player);
                }
                else if (choice == "4")
                {
                    if (player.Gold >= 10)
                    {
                        Consumable.Heal(player);
                    }
                    else
                    {
                        Console.WriteLine("You poor shit.", Color.Gold);
                    }
                }
                else if (choice == "5")
                {
                    Console.WriteLine("You leave...");
                }
                Console.WriteLine("-------", Color.GhostWhite);
            } while (choice != "5");
                  
        }

        private static void Trade(Player player, Weapon wep)
        {
            if (player.Gold >= wep.Cost)
            {
                player.Gold -= wep.Cost;
                player.SetWeapon(wep);
                player.EquipWeapon();
                Console.WriteLine($"Success! Now you have {wep.Name} equipped!", Color.Gold);
            }
            else
            {
                Console.WriteLine("You dont have enough gold!", Color.Gold);
            }
        }

        private static void Trade(Player player)
        {
            if (player.Gold >= player.Potion.Cost)
            {
                player.Gold -= player.Potion.Cost;
                player.Potion.Amount += 1;
                Console.WriteLine($"Success! Now you have {player.Potion.Amount}", Color.Gold);
            }
            else
            {
                Console.WriteLine("You dont have enough gold!", Color.Gold);
            }
        }

        
        // Items used in game:
        // Shields
        // I could make an array and implement that array in shop but im too lazy now meh.
        static Weapon shield1 = new Weapon("Wooden Shield", "Shield", 50, 25, 30);
        static Weapon shield2 = new Weapon("Steel Shield", "Shield", 60, 30, 60);
        static Weapon shield3 = new Weapon("Ebony Shield", "Shield", 70, 35, 100);
        static Weapon shield4 = new Weapon("Diamond Shield", "Shield", 80, 40, 180);
        static Weapon shield5 = new Weapon("Eternal Shield", "Shield", 95, 50, 300);

        // Daggers
        static Weapon dagger1 = new Weapon("Wooden Daggers", "Daggers", 55, 0, 30);
        static Weapon dagger2 = new Weapon("Steel Daggers", "Daggers", 65, 0, 60);
        static Weapon dagger3 = new Weapon("Ebony Daggers", "Daggers", 75, 0, 100);
        static Weapon dagger4 = new Weapon("Diamond Daggers", "Daggers", 85, 0, 180);
        static Weapon dagger5 = new Weapon("Eternal Daggers", "Daggers", 100, 0, 300);
    }
}
