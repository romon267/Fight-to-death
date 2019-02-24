using System;
using System.Drawing;
using Console = Colorful.Console;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightToDeath
{
    class Consumable : Item
    {
        public double Effect;
        public int Amount;

        public Consumable(string name, int effect, int amount)
        {
            Name = name;
            Effect = effect;
            Amount = amount;
        }

        public static void usePotion(int potionsLeft, Warrior war)
        {
            if (potionsLeft == 0)
            {
                Console.WriteLine($"{war.Name} desperatly tries to find potion but there are none left on his belt!", Color.Tomato);
            }
            else if (potionsLeft > 0)
            {
                Heal(war);
            }
        }

        // Has a 75% to heal -> 25% to critical heal and 25% to fail.
        public static void Heal(Warrior warrior)
        {
            int healChance = Battle.RollDice(21);
            if (healChance <= 5)
            {
                Console.WriteLine($"{warrior.Name} can't open a bottle with potion, holy shit.", Color.Tomato);
            }

            else if (healChance >= 6 && healChance <= 18)
            {
                warrior.Potion.Amount -= 1;
                double healEffect = warrior.Potion.Effect * 0.7;
                warrior.Health += healEffect;
                Console.WriteLine($"{warrior.Name} succesfully used a {warrior.Potion.Name} and gained {healEffect} health,\nand now has {warrior.Health} health.", Color.YellowGreen);
            }

            else
            {
                warrior.Potion.Amount -= 1;
                warrior.Health += warrior.Potion.Effect;
                Console.WriteLine($"{warrior.Name} critically used a {warrior.Potion.Name} and gained {warrior.Potion.Effect} health,\nand now has {warrior.Health} health!!!", Color.Yellow);
            }
        }
    }

}
