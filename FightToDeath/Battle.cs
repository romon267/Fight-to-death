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

        
        

        public static void Combat(Warrior warA, Warrior warB)
        {
            if (warA.Health <= 0 || warB.Health <= 0)
            {
                if (warA.Health > 0 && warB.Health <= 0)
                {
                    Console.WriteLine($"{warB.Name} died and {warA.Name} has won.");
                }
                else if (warB.Health > 0 && warA.Health <= 0)
                {
                    Console.WriteLine($"{warA.Name} died and {warB.Name} has won.");
                }
                
                Console.ReadLine(); // so the shit doesnt close
                
            }
            else
            {
                // Decide who attacks first in every turn, 50/50 chance
                if (RollDice(3) == 1)
                {
                    Console.WriteLine("Turn {0}:", turnCounter);
                    Console.WriteLine($"{warA.Name} got a chance to strike first!",  Color.Orange);
                    int warAAtk = CalcDmg(warA, warB);
                    warB.Health -= warAAtk;
                    Console.WriteLine($"{warA.Name} dealt {warAAtk} damage.", Color.Red);
                }
                else
                {
                    Console.WriteLine("Turn {0}:", turnCounter);
                    Console.WriteLine($"{warB.Name} got a chance to strike first!", Color.LightGreen);
                    int warBAtk = CalcDmg(warB, warA);
                    warA.Health -= warBAtk;
                    Console.WriteLine($"{warB.Name} dealt {warBAtk} damage.", Color.Red);                
                }
                Console.WriteLine($"{warB.Name} has now {warB.Health} health left.");
                Console.WriteLine($"{warA.Name} has now {warA.Health} health left.");
                if (warA.Health < 50 && warA.Health > 0)
                {
                    Consumable.usePotion(warA.MaxPotions, warA);
                }
                if (warB.Health < 50 && warB.Health > 0)
                {
                    Consumable.usePotion(warB.MaxPotions, warB);
                }
                Console.WriteLine("--------");
                turnCounter++;
                Combat(warA, warB);
            }
        }

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