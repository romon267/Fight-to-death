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

        public Consumable(string name, int effect, int amount, double cost)
        {
            Name = name;
            Effect = effect;
            Amount = amount;
            Cost = cost;
        }

        // Check if there are potions left and if yes -> trigger heal(), Player overload.
        public static void usePotion(int potionsLeft, Player war)
        {
            if (potionsLeft == 0)
            {
                Console.WriteLine($"{war.Name} отчаянно пытается найти зелья но у него их не осталось!", Color.Tomato);
            }
            else if (potionsLeft > 0)
            {
                Heal(war);
            }
        }

        // Enemy overload
        public static void usePotion(int potionsLeft, Enemy war)
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
        public static void Heal(Player warrior)
        {
            int healChance = Battle.RollDice(21);
            if (healChance <= 5)
            {
                Console.WriteLine($"{warrior.Name} не может открыть бутылку с зельем.", Color.Tomato);
            }

            else if (healChance >= 6 && healChance <= 18)
            {
                warrior.Potion.Amount -= 1;
                double healEffect = warrior.Potion.Effect * 0.7;
                warrior.Health += healEffect;
                Console.WriteLine($"{warrior.Name} успешно использовал зелье и восстановил {healEffect} здовровья,\nи теперь у него: {warrior.Health} здоровья.", Color.YellowGreen);
            }

            else
            {
                warrior.Potion.Amount -= 1;
                warrior.Health += warrior.Potion.Effect;
                Console.WriteLine($"{warrior.Name} удачно открыл бутылку с зельем и восстановил {warrior.Potion.Effect} здоровья,\nтеперь у него: {warrior.Health} здоровья!", Color.Yellow);
            }
        }
        // overload for enemy type
        public static void Heal(Enemy warrior)
        {
            int healChance = Battle.RollDice(21);
            if (healChance <= 5)
            {
                Console.WriteLine($"{warrior.Name} не может открыть бутылку с зельем.", Color.Tomato);
            }

            else if (healChance >= 6 && healChance <= 18)
            {
                warrior.Potion.Amount -= 1;
                double healEffect = warrior.Potion.Effect * 0.7;
                warrior.Health += healEffect;
                Console.WriteLine($"{warrior.Name} успешно использовал зелье и восстановил {healEffect} здовровья,\nи теперь у него: {warrior.Health} здоровья.", Color.YellowGreen);
            }

            else
            {
                warrior.Potion.Amount -= 1;
                warrior.Health += warrior.Potion.Effect;
                Console.WriteLine($"{warrior.Name} удачно открыл бутылку с зельем и восстановил {warrior.Potion.Effect} здоровья,\nтеперь у него: {warrior.Health} здоровья!", Color.Yellow);
            }
        }
    }

}
