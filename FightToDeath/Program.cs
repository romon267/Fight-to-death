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
        static Enemy enemy1 = new Enemy("Боб Тэйбор", 150, 0, 0, 120);

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
            Console.WriteLine("Нажмите \"enter\", чтобы начать игру.", Color.AntiqueWhite);
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
            Console.WriteLine($"Вы убили {Battle.enemiesKilled} врагов(а)!");
            Battle.ResetChar(player);
            Console.ReadLine();
        }

        public static void EnterNewLevel()
        {
            Console.WriteLine($"Уровень: {Battle.enemiesKilled + 1}");
            Console.WriteLine("Вы выходите на арену, вы видите вашего врага и зрителей на трибунах.", Color.GhostWhite);
            Console.WriteLine("Вы приближаетесь... Нажмите \"enter\" чтобы начать бой!", Color.GhostWhite);
            Console.ReadLine();
        }

        public static void CharacterCreation(Player player, Enemy enemy)
        {
            Console.WriteLine("Добро пожаловать на арену, боец!", Color.AntiqueWhite);
            Console.WriteLine("Как тебя зовут?", Color.AntiqueWhite);
            string playerName;
            do
            {
                playerName = Console.ReadLine();
                if (playerName == "" || playerName == " ")
                {
                    Console.WriteLine("Введите любое имя.");
                }
            } while (playerName != "" ^ playerName != " ");
            
            player.Name = playerName;
            Console.WriteLine($"Здравствуй, {player.Name}!", Color.AntiqueWhite);
            Console.WriteLine("Какой ты расы, эльф или дворф? Введите \"elf\" или \"dwarf\"", Color.AntiqueWhite);
            string playerRace;
            do
            {
                playerRace = Console.ReadLine();
                if (playerRace.ToLower() == "elf")
                {
                    Console.WriteLine("*Непонятные эльфийские слова*", Color.AntiqueWhite);
                    player.Health = 120;
                    player.BaseAttack += 15;
                }
                else if (playerRace.ToLower() == "dwarf")
                {
                    player.Health += 170;
                    player.BaseBlock += 10;
                    Console.WriteLine("Дворф!", Color.AntiqueWhite);
                }
                else
                {
                    Console.WriteLine("Выбери расу: \"elf\" или \"dwarf\"");
                }
            } while (playerRace != "elf" && playerRace != "dwarf");
            string playerWep;
            do
            {


                Console.WriteLine("Какое оружие предпочитаешь? Кинжалы(\"daggers\") или щит с мечом(\"shield\")?");
                playerWep = Console.ReadLine();
                if (playerWep.ToLower() == "shield")
                {
                    player.SetWeapon(shield1);
                    player.EquipWeapon();
                    Console.WriteLine("Крепкий щит!");
                }
                else if (playerWep.ToLower() == "daggers")
                {
                    player.SetWeapon(dagger1);
                    player.EquipWeapon();
                    Console.WriteLine("Порви врага критами!");
                }
                else
                {
                    Console.WriteLine("Введите \"daggers\" или \"shield\"");
                }
            } while (playerWep != "shield" && playerWep != "daggers");
            Console.WriteLine("-------");
            Console.WriteLine(@"Каждый ваш ход вы можете совершить одно действие:
Атаковать врага, использовать специальное действие вашего оружия - щит дает возможность поднять щит,
что защитит от следующей атаки врага, кинжалы дают возможность заточить клинки, что гарантирует
критический урон от вашей следующей атаки.
Также можно использовать зелье здоровья и посмотреть информацию о том сколько зельев осталось
у вас и у врага и какие усиления сейчас действуют.", Color.AntiqueWhite);
            Console.WriteLine($"{player.Name}, ваша раса: {playerRace}, ваше оружие: {player.Weapon.Name}, удачи!");
            Console.WriteLine("-------");
            Console.WriteLine("Ваш первый враг:");
            enemy.SetWeapon(shield1);
            enemy.EquipWeapon();
            Console.WriteLine($"{enemy.Name}, оружие: {enemy.Weapon.Name}.");
            Console.ReadLine();
        }
        
        public static void Shop(Player player)
        {
            string choice;
            do
            {

                Console.WriteLine("$$$$$$$", Color.Gold);
                Console.WriteLine($"Добро пожаловать в лавку, чемпион, выбирай товары!", Color.Gold);
                Console.WriteLine($"Ваше золото: {player.Gold}", Color.Gold);
                Console.WriteLine("1. Оружие\n2. Броня\n3. Зелья\n4. Лечение за 10 золотых\n5. Уйти", Color.Gold);
                choice = Console.ReadLine();
                Console.WriteLine("-------", Color.GhostWhite);
                if (choice == "1")
                {
                    Console.WriteLine($"1.{shield2.Name}: {shield2.Cost} золотых.\n2.{shield3.Name}: {shield3.Cost} золотых.", Color.AliceBlue);
                    Console.WriteLine($"3.{shield4.Name}: {shield4.Cost} золотых.\n4.{shield5.Name}: {shield5.Cost} золотых.", Color.AliceBlue);
                    Console.WriteLine("-------");
                    Console.WriteLine($"5.{dagger2.Name}: {dagger2.Cost} золотых.\n6.{dagger3.Name}: {dagger3.Cost} золотых.", Color.BlueViolet);
                    Console.WriteLine($"7.{dagger4.Name}: {dagger4.Cost} золотых.\n8.{dagger5.Name}: {dagger5.Cost} золотых.", Color.BlueViolet);

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
                    Console.WriteLine("Брони нет лооол, просто используй лечение.", Color.OrangeRed);
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
                        Console.WriteLine("Не хватает золота.", Color.Gold);
                    }
                }
                else if (choice == "5")
                {
                    Console.WriteLine("Вы уходите...");
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
                Console.WriteLine($"Успех! Теперь на вас: {wep.Name}!", Color.Gold);
            }
            else
            {
                Console.WriteLine("Не хватает золота!", Color.Gold);
            }
        }

        private static void Trade(Player player)
        {
            if (player.Gold >= player.Potion.Cost)
            {
                player.Gold -= player.Potion.Cost;
                player.Potion.Amount += 1;
                Console.WriteLine($"Успех! Ваши зелья: {player.Potion.Amount}", Color.Gold);
            }
            else
            {
                Console.WriteLine("Не хватает золота!", Color.Gold);
            }
        }

        
        // Items used in game:
        // Shields
        // I could make an array and implement that array in shop but im too lazy now meh.
        static Weapon shield1 = new Weapon("Деревянный щит", "Shield", 50, 25, 30);
        static Weapon shield2 = new Weapon("Железный шит", "Shield", 60, 30, 60);
        static Weapon shield3 = new Weapon("Драконий щит", "Shield", 70, 35, 100);
        static Weapon shield4 = new Weapon("Алмазный щит", "Shield", 80, 40, 180);
        static Weapon shield5 = new Weapon("Вечный щит", "Shield", 95, 50, 300);

        // Daggers
        static Weapon dagger1 = new Weapon("Деревянные кинжалы", "Daggers", 55, 0, 30);
        static Weapon dagger2 = new Weapon("Железные кинжалы", "Daggers", 65, 0, 60);
        static Weapon dagger3 = new Weapon("Драконьи кинжалы", "Daggers", 75, 0, 100);
        static Weapon dagger4 = new Weapon("Алмазные кинжалы", "Daggers", 85, 0, 180);
        static Weapon dagger5 = new Weapon("Вечные кинжалы", "Daggers", 100, 0, 300);
    }
}
