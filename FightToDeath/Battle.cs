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
        static Random rnd = new Random();

        public static void StartFight(Player WarriorA, Enemy WarriorB)
        {
            turnCounter = 1;
            Combat(WarriorA, WarriorB);
        }

        
        
        // Main Combat function.
        public static void Combat(Player player, Enemy enemy)
        {
            if (player.Health <= 0 || enemy.Health <= 0)
            {
                if (player.Health > 0 && enemy.Health <= 0)
                {
                    Console.WriteLine($"{enemy.Name} died and {player.Name} has won. Congrats!", Color.Yellow);
                }
                else if (enemy.Health > 0 && player.Health <= 0)
                {
                    Console.WriteLine($"{player.Name} died and {enemy.Name} has won.", Color.OrangeRed);
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
                    Console.WriteLine("2. Raise Shield.", Color.AntiqueWhite);
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
                        else if (answer == "2")
                        {
                            player.RaiseShield = true;
                            Console.WriteLine("You raised the shield. Next enemy attack will be entirely blocked!", Color.LightGreen);
                            break;
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
                    else if (enemyAction == 3)
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
                                    
                }
                else
                {
                    Combat(player, enemy);
                }
                
                Console.WriteLine($"{player.Name} has now {player.Health} health left.", Color.IndianRed);
                Console.WriteLine($"{enemy.Name} has now {enemy.Health} health left.", Color.DarkOrange);

                Console.WriteLine("--------", Color.PaleTurquoise);
                
                turnCounter++;
                Thread.Sleep(2000);
                Combat(player, enemy);
            }
        }

        // Player attacking enemy
        public static void Attack(Player warA, Enemy warB)
        {
            int attackChance = RollDice(21);
            if (attackChance <= 5)
            {
                Console.WriteLine($"{warA.Name} missed!", Color.Teal);
            }
            else
            {
                int strike = RollDice(21);
                if (strike <= 5 || warB.RaiseShield == true)
                {
                    warB.RaiseShield = false;
                    Console.WriteLine($"{warB.Name} blocked entire blow!", Color.BlanchedAlmond);
                }
                else if (strike >= 6 && strike <= 10)
                {
                    double damage = (warA.BaseAttack - warB.BaseBlock);
                    warB.Health -= damage;
                    Console.WriteLine($"{warB.Name} partially blocked and suffered {damage} damage.", Color.PaleVioletRed);
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
                    Console.WriteLine($"{warA.Name} landed a critical strike dealing {damage}!!!", Color.DarkRed);
                }
            }
        }

        // Enemy attacking player overolad
        public static void Attack(Enemy warA, Player warB)
        {
            int attackChance = RollDice(21);
            if (attackChance <= 5)
            {
                Console.WriteLine($"{warA.Name} missed!", Color.Teal);
            }
            else
            {
                int strike = RollDice(21);
                if (strike <= 5 || warB.RaiseShield == true)
                {
                    warB.RaiseShield = false;
                    Console.WriteLine($"{warB.Name} blocked entire blow!", Color.BlanchedAlmond);
                }
                else if (strike >= 6 && strike <= 10)
                {
                    double damage = (warA.BaseAttack - warB.BaseBlock);
                    warB.Health -= damage;
                    Console.WriteLine($"{warB.Name} partially blocked and suffered {damage} damage.", Color.PaleVioletRed);
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
                    Console.WriteLine($"{warA.Name} landed a critical strike dealing {damage}!!!", Color.DarkRed);
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