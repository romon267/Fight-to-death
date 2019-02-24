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
        static Random rnd = new Random();

        public static void StartFight(Warrior WarriorA, Warrior WarriorB)
        {
            turnCounter = 1;
            Combat(WarriorA, WarriorB);
        }

        
        
        // Main Combat function.
        public static void Combat(Warrior player, Warrior enemy)
        {
            if (player.Health <= 0 || enemy.Health <= 0)
            {
                if (player.Health > 0 && enemy.Health <= 0)
                {
                    Console.WriteLine($"{enemy.Name} died and {player.Name} has won.");
                }
                else if (enemy.Health > 0 && player.Health <= 0)
                {
                    Console.WriteLine($"{player.Name} died and {enemy.Name} has won.");
                }
                
                Console.ReadLine(); // so the shit doesnt close
                
            }
            else
            {
                // Decide who attacks first in every turn, 50/50 chance, if rolldice is 1 then its players turn, 2 - ai.
                if (RollDice(3) == 1)
                {
                    Console.WriteLine("Turn {0}:", turnCounter);
                    Console.WriteLine($"{player.Name} got a chance to strike first!", Color.Orange);
                    Console.WriteLine("What will you do?");
                    Console.WriteLine("1. Attack.");
                    Console.WriteLine("2. Drink potion.");
                    Console.WriteLine("3. Info.");
                    string answer;
                    do
                    {
                        answer = Console.ReadLine();

                        if (answer == "1")
                        {
                            int playerAtk = CalcDmg(player, enemy);
                            enemy.Health -= playerAtk;
                            Console.WriteLine($"{player.Name} dealt {playerAtk} damage.", Color.Red);
                            break;
                        }
                        else if (answer == "2")
                        {
                            Consumable.usePotion(player.Potion.Amount, player);
                            break;
                        }
                        else if (answer == "3")
                        {
                            Console.WriteLine($"Your health: {player.Health}");
                            Console.WriteLine($"{enemy.Name}'s health: {enemy.Health}");
                            Console.WriteLine($"You have {player.Potion.Amount} potions left.");
                            Console.WriteLine($"{enemy.Name} has {enemy.Potion.Amount} potions left.");
                        }
                    } while (answer == "3");
                }
                else
                {
                    // Enemy turn description
                    Console.WriteLine("Turn {0}:", turnCounter);
                    Console.WriteLine($"{enemy.Name} got a chance to strike first!", Color.LightGreen);
                    int enemyAction = RollDice(3);
                    if (enemyAction == 1)
                    {
                        int enemyAtk = CalcDmg(enemy, player);
                        player.Health -= enemyAtk;
                        Console.WriteLine($"{enemy.Name} dealt {enemyAtk} damage.", Color.Red);
                    }
                    else if (enemyAction == 2)
                    {
                        if (enemy.Health < 50 && enemy.Health > 0)
                        {
                            Consumable.usePotion(enemy.Potion.Amount, enemy);
                        }
                        else if (enemy.Health > 50)
                        {
                            int enemyAtk = CalcDmg(enemy, player);
                            player.Health -= enemyAtk;
                            Console.WriteLine($"{enemy.Name} dealt {enemyAtk} damage.", Color.Red);
                        }
                    }
                                    
                }
                Console.WriteLine($"{enemy.Name} has now {enemy.Health} health left.");
                Console.WriteLine($"{player.Name} has now {player.Health} health left.");
                
                Console.WriteLine("--------");
                turnCounter++;
                Thread.Sleep(2000);
                Combat(player, enemy);
            }
        }

        // Calculates damage: Attack value - Block value.
        public static int CalcDmg(Warrior warA, Warrior warB)
        {
            int Damage = warA.GenerateAttack() - warB.GenerateBlock();
            if (Damage > 0)
            {
                return Damage;
            }
            else { return Damage = 0; };
        }

       

        // Just gives back a random number between zero and max - u can set max.
        public static int RollDice(int max)
        {
            return rnd.Next(1, max);
        }

        
    }
}