using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightToDeath
{
    class Warrior
    {
        public string Name { get; set; } = "Warrior";
        public int Health { get; set; } = 0;
        public int MaxAttack { get; set; } = 0;
        public int MaxBlock { get; set; } = 0;
        public int MaxPotions { get; set; } = 0;

        // new random class
        Random rnd = new Random();

        // Constructor
        public Warrior(string name = "Warrior",
            int health = 0,
            int maxAttack = 0,
            int maxBlock = 0,
            int maxPotions = 0)
        {
            Name = name;
            Health = health;
            MaxAttack = maxAttack;
            MaxBlock = maxBlock;
            MaxPotions = maxPotions;
        }

        // random attack number generator
        public int GenerateAttack()
        {
            return rnd.Next(1, MaxAttack);
        }

        //random block num gen
        public int GenerateBlock()
        {
            return rnd.Next(1, MaxBlock);
        }
    }
}
