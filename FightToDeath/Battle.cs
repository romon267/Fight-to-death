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
        static int fightDone = 0;
        static Random rnd = new Random();

        public static void ResetChar(Enemy enemy)
        {
            enemy.Health = 150;
            enemy.Potion.Amount = 3;
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
                    Console.WriteLine($"{enemy.Name} died and {player.Name} has won. Congrats!", Color.Yellow);
                    player.Gold += enemy.Gold;
                    fightDone += 1;
                    ResetChar(enemy);
                }
                else if (enemy.Health > 0 && player.Health <= 0)
                {
                    Console.WriteLine($"{player.Name} died and {enemy.Name} has won.", Color.OrangeRed);
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
                    Console.WriteLine("Turn {0}:", turnCounter, Color.AntiqueWhite);
                    Console.WriteLine($"{player.Name} sees a chance to make a move!", Color.LightGreen);
                    Console.WriteLine("What will you do?", Color.AntiqueWhite);
                    Console.WriteLine("1. Attack.", Color.AntiqueWhite);
                    if (player.Weapon.Type == "Shield")
                    {
                        Console.WriteLine("2. Raise Shield.", Color.AntiqueWhite);
                    }
                    else if (player.Weapon.Type == "Daggers")
                    {
                        Console.WriteLine("2. Sharp your daggers");
                    }
                    Console.WriteLine("3. Drink potion.", Color.AntiqueWhite);
                    Console.WriteLine("4. Info.", Color.AntiqueWhite);
                    string answer;
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
                            Console.WriteLine("You raised the shield. Next enemy attack will be entirely blocked!", Color.LightGreen);
                            break;
                        }
                        else if (answer == "2" && player.Weapon.Type == "Daggers")
                        {
                            player.SharpDaggers = true;
                            Console.WriteLine("You sharped your daggers, next attack will be guaranteed crit!", Color.AntiqueWhite);
                        }
                        else if (answer == "3")
                        {
                            Consumable.usePotion(player.Potion.Amount, player);
                            break;
                        }
                        else if (answer == "4")
                        {
                            Console.WriteLine($"Your health: {player.Health}", Color.IndianRed);
                            Console.WriteLine($"{enemy.Name}'s health: {enemy.Health}", Color.DarkOrange);
                            Console.WriteLine($"You have {player.Potion.Amount} potions left.", Color.LightGoldenrodYellow);
                            Console.WriteLine($"{enemy.Name} has {enemy.Potion.Amount} potions left.", Color.LightGoldenrodYellow);
                            if (player.SharpDaggers == true || player.RaiseShield == true)
                            {
                                Console.WriteLine("You have some augmentation.", Color.AntiqueWhite);
                            }
                            if (enemy.SharpDaggers == true || enemy.RaiseShield == true)
                            {
                                Console.WriteLine("Enemy has some augmentation, prepare anus.", Color.AntiqueWhite);

                            }
                        }
                    } while (answer == "4");
                }
                else if (turnRoll == 2 && enemyTurnRow < 2)
                {
                    // Enemy turn description
                    playerTurnRow = 0;
                    enemyTurnRow += 1;
                    Console.WriteLine("Turn {0}:", turnCounter, Color.AntiqueWhite);
                    Console.WriteLine($"{enemy.Name} sees a chance to make a move!", Color.Orange);
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
                            Console.WriteLine($"{enemy.Name} is rising his shield to cover!", Color.Orange);
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
                            Console.WriteLine($"{enemy.Name} is sharping his daggers, hell no!", Color.Orange);
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
                    Console.WriteLine("Rerolling...", Color.AntiqueWhite);
                }
                
                Console.WriteLine($"{player.Name} has now {player.Health} health left.", Color.IndianRed);
                Console.WriteLine($"{enemy.Name} has now {enemy.Health} health left.", Color.DarkOrange);

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
                        Console.WriteLine($"{warA.Name} missed!", Color.Teal);
                    }
                    else
                    {
                        int strike = RollDice(21);
                        if (strike <= 3 || warB.RaiseShield == true)
                        {
                            warB.RaiseShield = false;
                            Console.WriteLine($"{warA.Name} couldn't pierce the stalwart defence of {warB.Name}\nhe blocked entire blow!", Color.BlanchedAlmond);
                        }
                        else if (strike >= 6 && strike <= 10)
                        {
                            double damage = (warA.BaseAttack - warB.BaseBlock);
                            warB.Health -= damage;
                            Console.WriteLine($"{warA.Name} attacked but {warB.Name} partially blocked\n and suffered {damage} damage.", Color.PaleVioletRed);
                        }
                        else if (strike >= 11 && strike <= 18)
                        {
                            warB.Health -= warA.BaseAttack;
                            Console.WriteLine($"{warA.Name} dealt {warA.BaseAttack} damage.", Color.Red);
                        }
                        else
                        {
                            double damage = warA.BaseAttack * 1.5;
                            warB.Health -= damage;
                            Console.WriteLine($"{warA.Name} landed a critical strike dealing {damage} damage!!!", Color.DarkRed);
                        }
                    }
                }

                else if (warB.Weapon.Type == "Daggers") // player SHIELD, enemy DAGGERS
                {
                    int attackChance = RollDice(21);
                    if (attackChance <= 5)
                    {
                        Console.WriteLine($"{warA.Name} missed, {warB.Name} is elusive!", Color.Teal);
                    }
                    else
                    {
                        int strike = RollDice(21);
                        if (strike <= 2)
                        {
                            Console.WriteLine($"{warB.Name} thrown a smoke bomb and scared the shit out of {warA.Name}.\n{warA.Name} missed.");
                        }
                        else if (strike >= 3 && strike <= 10)
                        {
                            double damage = (warA.BaseAttack * 0.6);
                            warB.Health -= damage;
                            Console.WriteLine($"{warA.Name} sliced flesh of {warB.Name} but {warB.Name} slipped away\nin the last moment and suffered {damage} damage.", Color.PaleVioletRed);
                        }
                        else if (strike >= 11 && strike <= 18)
                        {
                            warB.Health -= warA.BaseAttack;
                            Console.WriteLine($"{warA.Name} dealt {warA.BaseAttack} damage.", Color.Red);
                        }
                        else
                        {
                            double damage = warA.BaseAttack * 1.5;
                            warB.Health -= damage;
                            Console.WriteLine($"{warA.Name} landed a critical strike dealing {damage} damage!!!", Color.DarkRed);
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
                        Console.WriteLine($"{warA.Name} missed!", Color.Teal);
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
                                Console.WriteLine($"{warA.Name} couldn't pierce the stalwart defence of {warB.Name}\nhe blocked entire blow!", Color.BlanchedAlmond);

                            }
                            else
                            {
                                double damage = warA.BaseAttack * 1.5;
                                warB.Health -= damage;
                                Console.WriteLine($"{warA.Name} landed a critical strike dealing {damage} damage!!!", Color.DarkRed);
                            }
                        }
                        else if (strike <= 3 || warB.RaiseShield == true)
                        {
                            warB.RaiseShield = false;
                            Console.WriteLine($"{warA.Name} couldn't pierce the stalwart defence of {warB.Name}\nhe blocked entire blow!", Color.BlanchedAlmond);
                        }
                        else if (strike >= 6 && strike <= 10)
                        {
                            double damage = (warA.BaseAttack - warB.BaseBlock);
                            warB.Health -= damage;
                            Console.WriteLine($"{warA.Name} attacked but {warB.Name} partially blocked\n and suffered {damage} damage.", Color.PaleVioletRed);
                        }
                        else if (strike >= 11 && strike <= 14)
                        {
                            warB.Health -= warA.BaseAttack;
                            Console.WriteLine($"{warA.Name} dealt {warA.BaseAttack} damage.", Color.Red);
                        }
                    }
                }
                else if (warB.Weapon.Type == "Daggers") // player DAGGERS, enemy DAGGERS
                {
                    int attackChance = RollDice(21);
                    if (attackChance <= 5)
                    {
                        Console.WriteLine($"{warA.Name} missed, {warB.Name} is elusive!", Color.Teal);
                    }
                    else
                    {
                        int strike = RollDice(21);
                        if (strike >= 15 || warA.SharpDaggers == true)
                        {
                            warA.SharpDaggers = false;
                            double damage = warA.BaseAttack * 1.5;
                            warB.Health -= damage;
                            Console.WriteLine($"{warA.Name} landed a critical strike dealing {damage} damage!!!", Color.DarkRed);
                        }
                        else if (strike <= 2)
                        {
                            Console.WriteLine($"{warB.Name} thrown a smoke bomb and scared the shit out of {warA.Name}.\n{warA.Name} missed.");
                        }
                        else if (strike >= 3 && strike <= 10)
                        {
                            double damage = (warA.BaseAttack * 0.8);
                            warB.Health -= damage;
                            Console.WriteLine($"{warA.Name} sliced flesh of {warB.Name} but {warB.Name} slipped away\nin the last moment and suffered {damage} damage.", Color.PaleVioletRed);
                        }
                        else if (strike >= 11 && strike <= 18)
                        {
                            warB.Health -= warA.BaseAttack;
                            Console.WriteLine($"{warA.Name} dealt {warA.BaseAttack} damage.", Color.Red);
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
                        Console.WriteLine($"{warA.Name} missed!", Color.Teal);
                    }
                    else
                    {
                        int strike = RollDice(21);
                        if (strike <= 3 || warB.RaiseShield == true)
                        {
                            warB.RaiseShield = false;
                            Console.WriteLine($"{warA.Name} couldn't pierce the stalwart defence of {warB.Name}\nhe blocked entire blow!", Color.BlanchedAlmond);
                        }
                        else if (strike >= 6 && strike <= 10)
                        {
                            double damage = (warA.BaseAttack - warB.BaseBlock);
                            warB.Health -= damage;
                            Console.WriteLine($"{warA.Name} attacked but {warB.Name} partially blocked\n and suffered {damage} damage.", Color.PaleVioletRed);
                        }
                        else if (strike >= 11 && strike <= 18)
                        {
                            warB.Health -= warA.BaseAttack;
                            Console.WriteLine($"{warA.Name} dealt {warA.BaseAttack} damage.", Color.Red);
                        }
                        else
                        {
                            double damage = warA.BaseAttack * 1.5;
                            warB.Health -= damage;
                            Console.WriteLine($"{warA.Name} landed a critical strike dealing {damage} damage!!!", Color.DarkRed);
                        }
                    }
                }

                else if (warB.Weapon.Type == "Daggers") // enemy SHIELD, player DAGGERS
                {
                    int attackChance = RollDice(21);
                    if (attackChance <= 5)
                    {
                        Console.WriteLine($"{warA.Name} missed, {warB.Name} is elusive!", Color.Teal);
                    }
                    else
                    {
                        int strike = RollDice(21);
                        if (strike <= 2)
                        {
                            Console.WriteLine($"{warB.Name} thrown a smoke bomb and scared the shit out of {warA.Name}.\n{warA.Name} missed.");
                        }
                        else if (strike >= 3 && strike <= 10)
                        {
                            double damage = (warA.BaseAttack * 0.6);
                            warB.Health -= damage;
                            Console.WriteLine($"{warA.Name} sliced flesh of {warB.Name} but {warB.Name} slipped away\nin the last moment and suffered {damage} damage.", Color.PaleVioletRed);
                        }
                        else if (strike >= 11 && strike <= 18)
                        {
                            warB.Health -= warA.BaseAttack;
                            Console.WriteLine($"{warA.Name} dealt {warA.BaseAttack} damage.", Color.Red);
                        }
                        else
                        {
                            double damage = warA.BaseAttack * 1.5;
                            warB.Health -= damage;
                            Console.WriteLine($"{warA.Name} landed a critical strike dealing {damage} damage!!!", Color.DarkRed);
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
                        Console.WriteLine($"{warA.Name} missed!", Color.Teal);
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
                                Console.WriteLine($"{warA.Name} couldn't pierce the stalwart defence of {warB.Name}\nhe blocked entire blow!", Color.BlanchedAlmond);
                            }
                            else
                            {
                                double damage = warA.BaseAttack * 1.5;
                                warB.Health -= damage;
                                Console.WriteLine($"{warA.Name} landed a critical strike dealing {damage} damage!!!", Color.DarkRed);
                            }
                        }
                        else if (strike <= 3 || warB.RaiseShield == true)
                        {
                            warB.RaiseShield = false;
                            Console.WriteLine($"{warA.Name} couldn't pierce the stalwart defence of {warB.Name}\nhe blocked entire blow!", Color.BlanchedAlmond);
                        }
                        else if (strike >= 6 && strike <= 10)
                        {
                            double damage = (warA.BaseAttack - warB.BaseBlock);
                            warB.Health -= damage;
                            Console.WriteLine($"{warA.Name} attacked but {warB.Name} partially blocked\n and suffered {damage} damage.", Color.PaleVioletRed);
                        }
                        else if (strike >= 11 && strike <= 14)
                        {
                            warB.Health -= warA.BaseAttack;
                            Console.WriteLine($"{warA.Name} dealt {warA.BaseAttack} damage.", Color.Red);
                        }
                    }
                }
                else if (warB.Weapon.Type == "Daggers") // enemy DAGGERS, player DAGGERS
                {
                    int attackChance = RollDice(21);
                    if (attackChance <= 5)
                    {
                        Console.WriteLine($"{warA.Name} missed, {warB.Name} is elusive!", Color.Teal);
                    }
                    else
                    {
                        int strike = RollDice(21);
                        if (strike >= 15 || warA.SharpDaggers == true)
                        {
                            warA.SharpDaggers = false;
                            double damage = warA.BaseAttack * 1.5;
                            warB.Health -= damage;
                            Console.WriteLine($"{warA.Name} landed a critical strike dealing {damage} damage!!!", Color.DarkRed);
                        }
                        else if (strike <= 2)
                        {
                            Console.WriteLine($"{warB.Name} thrown a smoke bomb and scared the shit out of {warA.Name}.\n{warA.Name} missed.");
                        }
                        else if (strike >= 3 && strike <= 10)
                        {
                            double damage = (warA.BaseAttack * 0.8);
                            warB.Health -= damage;
                            Console.WriteLine($"{warA.Name} sliced flesh of {warB.Name} but {warB.Name} slipped away\nin the last moment and suffered {damage} damage.", Color.PaleVioletRed);
                        }
                        else if (strike >= 11 && strike <= 18)
                        {
                            warB.Health -= warA.BaseAttack;
                            Console.WriteLine($"{warA.Name} dealt {warA.BaseAttack} damage.", Color.Red);
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