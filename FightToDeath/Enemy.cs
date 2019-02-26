using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightToDeath
{
    class Enemy : Warrior
    {
        public Consumable Potion = new Consumable("Health Potion", 50, 3, 25);

        public Enemy(string name = "Enemy",
            double health = 0,
            double baseAttack = 0,
            double baseBlock = 0,
            double gold = 0)
        {
            Name = name;
            Health = health;
            BaseAttack = baseAttack;
            BaseBlock = baseBlock;
            Gold = gold;
        }

        
    }
}
