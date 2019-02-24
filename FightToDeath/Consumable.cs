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
        public int Amount;

        Consumable(string name, int amount)
        {
            Name = name;
            Amount = amount;
        }

        public static void usePotion(int potionsLeft, Warrior war)
        {
            if (potionsLeft == 0)
            {
                Console.WriteLine($"{war.Name} desperatly tries to find potion but there are none left on his belt", Color.Tomato);
            }
            else if (potionsLeft > 0)
            {
                Heal(war);
            }
        }

        // Has a 50% chance to heal.
        public static void Heal(Warrior warrior)
        {
            if (Battle.RollDice(3) == 2)
            {
                warrior.MaxPotions -= 1;
                warrior.Health += 50;
                Console.WriteLine($"{warrior.Name} succesfully used a healing potion and gained 50 health,\nand now has {warrior.Health} health.", Color.YellowGreen);
            }
            else
            {
                Console.WriteLine($"{warrior.Name} can't open a bottle with potion, holy shit.", Color.Tomato);
            }
        }
    }

}
