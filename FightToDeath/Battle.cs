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
    class Battle
    {
        static int turnCounter = 1;
        static int playerTurnRow = 0;
        static int enemyTurnRow = 0;
        public static int fightDone = 0;
        public static int enemiesKilled = 0;
        static Random rnd = new Random();

        public static void ResetChar(Enemy enemy)
        {
            enemy.Health = 150;
            enemy.Potion.Amount = 3;
            enemy.Gold = 100;
        }
        public static void ResetChar(Player player)
        {
            Program.playerDead = false;
            player.Health = 100;
            player.Potion.Amount = 3;
            player.Gold = 0;
        }


        public static void StartFight(Player WarriorA, Enemy WarriorB)
        {
            turnCounter = 1;
            fightDone = 0;
            do
            {
                Combat(WarriorA, WarriorB);

            } while (fightDone == 0);
        }

        
        
        // Main Combat function.
        public static void Combat(Player player, Enemy enemy)
        {
            if (player.Health <= 0 || enemy.Health <= 0)
            {
                if (player.Health > 0 && enemy.Health <= 0)
                {
                    Console.WriteLine($"{enemy.Name} умер и {player.Name} победил. Четко!", Color.Yellow);
                    player.Gold += enemy.Gold;
                    fightDone += 1;
                    enemiesKilled += 1;
                    ResetChar(enemy);
                    for (int i = 0; i < enemiesKilled; i++)
                    {
                        enemy.Health += 50;
                        enemy.BaseAttack += 15;
                        enemy.Gold += 50;
                    }
                    int nextEnemyWep = RollDice(3);
                    if (nextEnemyWep == 1)
                    {
                        enemy.Weapon.Name = "Stupid shield";
                        enemy.Weapon.Type = "Shield";
                    }
                    else
                    {
                        enemy.Weapon.Name = "Stupid daggers";
                        enemy.Weapon.Type = "Daggers";
                    }
                }
                else if (enemy.Health > 0 && player.Health <= 0)
                {
                    Console.WriteLine($"{player.Name} умер и {enemy.Name} победил. рип", Color.OrangeRed);
                    fightDone += 1;
                    Program.playerDead = true;
                    
                }
                
                Console.ReadLine(); // so the shit doesnt close. 
            }
            else
            {
                int turnRoll = RollDice(3);
                // Decide who attacks first in every turn, 50/50 chance, if rolldice is 1 then its players turn, 2 - ai.
                if (turnRoll == 1 && playerTurnRow < 2)
                {
                    playerTurnRow += 1;
                    enemyTurnRow = 0;
                    Console.WriteLine("Ход {0}:", turnCounter, Color.AntiqueWhite);
                    Console.WriteLine($"Ваш ход!", Color.LightGreen);
                    Console.WriteLine("Что будете делать?", Color.AntiqueWhite);
                    Console.WriteLine("1. Атака.", Color.AntiqueWhite);
                    if (player.Weapon.Type == "Shield")
                    {
                        Console.WriteLine("2. Поднять щит.", Color.AntiqueWhite);
                    }
                    else if (player.Weapon.Type == "Daggers")
                    {
                        Console.WriteLine("2. Заточить клинки.");
                    }
                    Console.WriteLine("3. Выпить зелье.", Color.AntiqueWhite);
                    Console.WriteLine("4. Информация.", Color.AntiqueWhite);
                    string answer;
                    do
                    {
                        do
                        {
                            answer = Console.ReadLine();

                            if (answer == "1")
                            {
                                Attack(player, enemy);
                                break;
                            }
                            else if (answer == "2" && player.Weapon.Type == "Shield")
                            {
                                player.RaiseShield = true;
                                Console.WriteLine("Вы подняли щит. Следующая атака врага будет полностью блокирована!", Color.LightGreen);
                                break;
                            }
                            else if (answer == "2" && player.Weapon.Type == "Daggers")
                            {
                                player.SharpDaggers = true;
                                Console.WriteLine("Вы заточили клинки, следующая атака будет критической!", Color.AntiqueWhite);
                            }
                            else if (answer == "3")
                            {
                                Consumable.usePotion(player.Potion.Amount, player);
                                break;
                            }
                            else if (answer == "4")
                            {
                                Console.WriteLine($"Ваше здоровье: {player.Health}", Color.IndianRed);
                                Console.WriteLine($"Здоровье врага: {enemy.Health}", Color.DarkOrange);
                                Console.WriteLine($"У вас осталось {player.Potion.Amount} зелья.", Color.LightGoldenrodYellow);
                                Console.WriteLine($"У врага еще {enemy.Potion.Amount} зелья.", Color.LightGoldenrodYellow);
                                if (player.SharpDaggers == true || player.RaiseShield == true)
                                {
                                    Console.WriteLine("У вас есть усиления (поднят щит или заточка клинков).", Color.AntiqueWhite);
                                }
                                if (enemy.SharpDaggers == true || enemy.RaiseShield == true)
                                {
                                    Console.WriteLine("У врага есть усиления.", Color.AntiqueWhite);

                                }
                            }
                            else
                            {
                                Console.WriteLine("Выберите что-то!", Color.GhostWhite);
                            }
                        } while (answer == "4");
                    } while (answer != "1" && answer != "2" && answer != "3" && answer != "4");
                }
                else if (turnRoll == 2 && enemyTurnRow < 2)
                {
                    // Enemy turn description
                    playerTurnRow = 0;
                    enemyTurnRow += 1;
                    Console.WriteLine("Ход {0}:", turnCounter, Color.AntiqueWhite);
                    Console.WriteLine($"{enemy.Name} видит шанс нанести удар!", Color.Orange);
                    int enemyAction = RollDice(4);
                    if (enemyAction == 1)
                    {
                        Attack(enemy, player);
                    }
                    else if (enemyAction == 2)
                    {
                        if (enemy.Health <= 75 && enemy.Health > 0)
                        {
                            Consumable.usePotion(enemy.Potion.Amount, enemy);
                        }
                        else if (enemy.Health > 75)
                        {
                            Attack(enemy, player);
                        }
                    }
                    else if (enemyAction == 3 && enemy.Weapon.Type == "Shield")
                    {
                        if (enemy.RaiseShield == false)
                        {
                            enemy.RaiseShield = true;
                            Console.WriteLine($"{enemy.Name} поднимает свой щит!", Color.Orange);
                        }
                        else
                        {
                            Attack(enemy, player); // Some room for reimagining AI.
                        }
                    }
                    else if (enemyAction == 3 && enemy.Weapon.Type == "Daggers")
                    {
                        if (enemy.SharpDaggers == false)
                        {
                            enemy.SharpDaggers = true;
                            Console.WriteLine($"{enemy.Name} затачивает клинки!", Color.Orange);
                        }
                        else
                        {
                            Attack(enemy, player); // Some room for reimagining AI.
                        }
                    }

                }
                else
                {
                    if (playerTurnRow == 2)
                    {
                        enemyTurnRow = 0;
                    }
                    else
                    {
                        playerTurnRow = 0;
                    }
                    Console.WriteLine("Перебрасываем кости...", Color.AntiqueWhite);
                }
                
                Console.WriteLine($"Ваше здоровье: {player.Health}.", Color.IndianRed);
                Console.WriteLine($"Здоровье врага: {enemy.Health}.", Color.DarkOrange);

                Console.WriteLine("--------", Color.PaleTurquoise);
                
                turnCounter++;
                Thread.Sleep(2000);
            }
        }

        // Player attacking enemy
        public static void Attack(Player warA, Enemy warB)
        {
            // Attack if player has SHIELD
            if (warA.Weapon.Type == "Shield")
            {
                if (warB.Weapon.Type == "Shield") // player SHIELD, enemy SHIELD
                {
                    int attackChance = RollDice(21);
                    if (attackChance <= 3)
                    {
                        Console.WriteLine($"{warA.Name} промахнулся!", Color.Teal);
                    }
                    else
                    {
                        int strike = RollDice(21);
                        if (strike <= 3 || warB.RaiseShield == true)
                        {
                            warB.RaiseShield = false;
                            Console.WriteLine($"{warA.Name} не смог пробить щит врага.\n {warB.Name} отразил весь урон!", Color.BlanchedAlmond);
                        }
                        else if (strike >= 4 && strike <= 9)
                        {
                            double damage = (warA.BaseAttack - warB.BaseBlock);
                            warB.Health -= damage;
                            Console.WriteLine($"{warA.Name} нанес удар, но {warB.Name} частично блокировал\nи получил {damage} урона.", Color.PaleVioletRed);
                        }
                        else if (strike >= 10 && strike <= 18)
                        {
                            warB.Health -= warA.BaseAttack;
                            Console.WriteLine($"{warA.Name} нанес {warA.BaseAttack} урона.", Color.Red);
                        }
                        else
                        {
                            double damage = warA.BaseAttack * 1.5;
                            warB.Health -= damage;
                            Console.WriteLine($"{warA.Name} нанес критический удар - {damage} урона!!!", Color.DarkRed);
                        }
                    }
                }

                else if (warB.Weapon.Type == "Daggers") // player SHIELD, enemy DAGGERS
                {
                    int attackChance = RollDice(21);
                    if (attackChance <= 5)
                    {
                        Console.WriteLine($"{warA.Name} промахнулся.", Color.Teal);
                    }
                    else
                    {
                        int strike = RollDice(21);
                        if (strike <= 2)
                        {
                            Console.WriteLine($"{warB.Name} кинул дымовую бомбу. {warA.Name} испугался и промахнулся.");
                        }
                        else if (strike >= 3 && strike <= 10)
                        {
                            double damage = (warA.BaseAttack * 0.6);
                            warB.Health -= damage;
                            Console.WriteLine($"{warA.Name} задел {warB.Name} но {warB.Name} ускользнул\nв последний момент и получил {damage} урона.", Color.PaleVioletRed);
                        }
                        else if (strike >= 11 && strike <= 18)
                        {
                            warB.Health -= warA.BaseAttack;
                            Console.WriteLine($"{warA.Name} нанес {warA.BaseAttack} урона.", Color.Red);
                        }
                        else
                        {
                            double damage = warA.BaseAttack * 1.5;
                            warB.Health -= (warB.BaseBlock - damage);
                            Console.WriteLine($"{warA.Name} нанес критический удар - {damage} урона!!!", Color.DarkRed);
                        }
                    }
                }
            }
            // attack if player has DAGGERS
            else if (warA.Weapon.Type == "Daggers")
            {
                if (warB.Weapon.Type == "Shield") // Player DAGGERS, enemy SHIELD
                {
                    int attackChance = RollDice(21);
                    if (attackChance <= 2)
                    {
                        Console.WriteLine($"{warA.Name} промахнулся!", Color.Teal);
                    }
                    else
                    {
                        int strike = RollDice(21);
                        if (strike >= 15 || warA.SharpDaggers == true)
                        {
                            warA.SharpDaggers = false;
                            if (warB.RaiseShield == true)
                            {
                                warB.RaiseShield = false;
                                double damage = (warA.BaseAttack * 1.5) * 0.5;
                                warB.Health -= damage;
                                Console.WriteLine($"{warA.Name} пробил щит врага и нанес {damage} урона!", Color.PaleVioletRed);

                            }
                            else
                            {
                                double damage = warA.BaseAttack * 1.5;
                                warB.Health -= damage;
                                Console.WriteLine($"{warA.Name} нанес критический удар - {damage} урона!!!", Color.DarkRed);
                            }
                        }
                        else if (strike <= 3 || warB.RaiseShield == true)
                        {
                            warB.RaiseShield = false;
                            Console.WriteLine($"{warA.Name} не смог пробить щит врага.\n {warB.Name} отразил весь урон!", Color.BlanchedAlmond);
                        }
                        else if (strike >= 4 && strike <= 9)
                        {
                            double damage = (warA.BaseAttack - warB.BaseBlock);
                            warB.Health -= damage;
                            Console.WriteLine($"{warA.Name} нанес удар, но {warB.Name} частично блокировал\nи получил {damage} урона.", Color.PaleVioletRed);
                        }
                        else if (strike >= 10 && strike <= 14)
                        {
                            warB.Health -= warA.BaseAttack;
                            Console.WriteLine($"{warA.Name} нанес {warA.BaseAttack} урона.", Color.Red);
                        }
                    }
                }
                else if (warB.Weapon.Type == "Daggers") // player DAGGERS, enemy DAGGERS
                {
                    int attackChance = RollDice(21);
                    if (attackChance <= 5)
                    {
                        Console.WriteLine($"{warA.Name} промахнулся.", Color.Teal);
                    }
                    else
                    {
                        int strike = RollDice(21);
                        if (strike >= 15 || warA.SharpDaggers == true)
                        {
                            warA.SharpDaggers = false;
                            double damage = warA.BaseAttack * 1.5;
                            warB.Health -= (warB.BaseBlock - damage);
                            Console.WriteLine($"{warA.Name} нанес критический удар - {damage} урона!!!", Color.DarkRed);
                        }
                        else if (strike <= 2)
                        {
                            Console.WriteLine($"{warB.Name} кинул дымовую бомбу. {warA.Name} испугался и промахнулся.");
                        }
                        else if (strike >= 3 && strike <= 10)
                        {
                            double damage = (warA.BaseAttack * 0.8);
                            warB.Health -= damage;
                            Console.WriteLine($"{warA.Name} задел {warB.Name} но {warB.Name} ускользнул\nв последний момент и получил {damage} урона.", Color.PaleVioletRed);
                        }
                        else if (strike >= 11 && strike <= 18)
                        {
                            warB.Health -= warA.BaseAttack;
                            Console.WriteLine($"{warA.Name} нанес {warA.BaseAttack} урона.", Color.Red);
                        }
                    }
                }
            }
        }

        // Enemy attacking player overolad
        public static void Attack(Enemy warA, Player warB)
        {
            // Attack if enemy has SHIELD
            if (warA.Weapon.Type == "Shield")
            {
                if (warB.Weapon.Type == "Shield") // enemy SHIELD, player SHIELD
                {
                    int attackChance = RollDice(21);
                    if (attackChance <= 3)
                    {
                        Console.WriteLine($"{warA.Name} промахнулся!", Color.Teal);
                    }
                    else
                    {
                        int strike = RollDice(21);
                        if (strike <= 3 || warB.RaiseShield == true)
                        {
                            warB.RaiseShield = false;
                            Console.WriteLine($"{warA.Name} не смог пробить щит врага.\n {warB.Name} отразил весь урон!", Color.BlanchedAlmond);
                        }
                        else if (strike >= 4 && strike <= 9)
                        {
                            double damage = (warA.BaseAttack - warB.BaseBlock);
                            warB.Health -= damage;
                            Console.WriteLine($"{warA.Name} нанес удар, но {warB.Name} частично блокировал\nи получил {damage} урона.", Color.PaleVioletRed);
                        }
                        else if (strike >= 10 && strike <= 18)
                        {
                            warB.Health -= warA.BaseAttack;
                            Console.WriteLine($"{warA.Name} нанес {warA.BaseAttack} урона.", Color.Red);
                        }
                        else
                        {
                            double damage = warA.BaseAttack * 1.5;
                            warB.Health -= damage;
                            Console.WriteLine($"{warA.Name} нанес критический удар - {damage} урона!!!", Color.DarkRed);
                        }
                    }
                }

                else if (warB.Weapon.Type == "Daggers") // enemy SHIELD, player DAGGERS
                {
                    int attackChance = RollDice(21);
                    if (attackChance <= 5)
                    {
                        Console.WriteLine($"{warA.Name} промахнулся.", Color.Teal);
                    }
                    else
                    {
                        int strike = RollDice(21);
                        if (strike <= 2)
                        {
                            Console.WriteLine($"{warB.Name} кинул дымовую бомбу. {warA.Name} испугался и промахнулся.");
                        }
                        else if (strike >= 3 && strike <= 10)
                        {
                            double damage = (warA.BaseAttack * 0.6);
                            warB.Health -= damage;
                            Console.WriteLine($"{warA.Name} задел {warB.Name} но {warB.Name} ускользнул\nв последний момент и получил {damage} урона.", Color.PaleVioletRed);
                        }
                        else if (strike >= 11 && strike <= 18)
                        {
                            warB.Health -= warA.BaseAttack;
                            Console.WriteLine($"{warA.Name} нанес {warA.BaseAttack} урона.", Color.Red);
                        }
                        else
                        {
                            double damage = warA.BaseAttack * 1.5;
                            warB.Health -= (warB.BaseBlock - damage);
                            Console.WriteLine($"{warA.Name} нанес критический удар - {damage} урона!!!", Color.DarkRed);
                        }
                    }
                }
            }
            // attack if player has DAGGERS
            else if (warA.Weapon.Type == "Daggers")
            {
                if (warB.Weapon.Type == "Shield") // enemy DAGGERS, player SHIELD
                {
                    int attackChance = RollDice(21);
                    if (attackChance <= 2)
                    {
                        Console.WriteLine($"{warA.Name} промахнулся!", Color.Teal);
                    }
                    else
                    {
                        int strike = RollDice(21);
                        if (strike >= 15 || warA.SharpDaggers == true)
                        {
                            warA.SharpDaggers = false;
                            if (warB.RaiseShield == true)
                            {
                                warB.RaiseShield = false;
                                double damage = (warA.BaseAttack * 1.5) * 0.5;
                                warB.Health -= damage;
                                Console.WriteLine($"{warA.Name} пробил щит врага и нанес {damage} урона!", Color.PaleVioletRed);
                            }
                            else
                            {
                                double damage = warA.BaseAttack * 1.5;
                                warB.Health -= damage;
                                Console.WriteLine($"{warA.Name} нанес критический удар - {damage} урона!!!", Color.DarkRed);
                            }
                        }
                        else if (strike <= 3 || warB.RaiseShield == true)
                        {
                            warB.RaiseShield = false;
                            Console.WriteLine($"{warA.Name} не смог пробить щит врага.\n {warB.Name} отразил весь урон!", Color.BlanchedAlmond);
                        }
                        else if (strike >= 4 && strike <= 9)
                        {
                            double damage = (warA.BaseAttack - warB.BaseBlock);
                            warB.Health -= damage;
                            Console.WriteLine($"{warA.Name} нанес удар, но {warB.Name} частично блокировал\nи получил {damage} урона.", Color.PaleVioletRed);
                        }
                        else if (strike >= 10 && strike <= 14)
                        {
                            warB.Health -= warA.BaseAttack;
                            Console.WriteLine($"{warA.Name} нанес {warA.BaseAttack} урона.", Color.Red);
                        }
                        
                    }
                }
                else if (warB.Weapon.Type == "Daggers") // enemy DAGGERS, player DAGGERS
                {
                    int attackChance = RollDice(21);
                    if (attackChance <= 5)
                    {
                        Console.WriteLine($"{warA.Name} промахнулся.", Color.Teal);
                    }
                    else
                    {
                        int strike = RollDice(21);
                        if (strike >= 15 || warA.SharpDaggers == true)
                        {
                            warA.SharpDaggers = false;
                            double damage = warA.BaseAttack * 1.5;
                            warB.Health -= (warB.BaseBlock - damage);
                            Console.WriteLine($"{warA.Name} нанес критический удар - {damage} урона!!!", Color.DarkRed);
                        }
                        else if (strike <= 2)
                        {
                            Console.WriteLine($"{warB.Name} кинул дымовую бомбу. {warA.Name} испугался и промахнулся.");
                        }
                        else if (strike >= 3 && strike <= 10)
                        {
                            double damage = (warA.BaseAttack * 0.8);
                            warB.Health -= damage;
                            Console.WriteLine($"{warA.Name} задел {warB.Name} но {warB.Name} ускользнул\nв последний момент и получил {damage} урона.", Color.PaleVioletRed);
                        }
                        else if (strike >= 11 && strike <= 18)
                        {
                            warB.Health -= warA.BaseAttack;
                            Console.WriteLine($"{warA.Name} нанес {warA.BaseAttack} урона.", Color.Red);
                        }
                    }
                }
            }
        }



        // Just gives back a random number between zero and max. Use it like a dice, for example set 21 for 20d dice;
        public static int RollDice(int max)
        {
            return rnd.Next(1, max);
        }

        
    }
}