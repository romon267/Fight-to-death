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
        public double Health { get; set; } = 0;
        public double BaseAttack { get; set; } = 0;
        public double BaseBlock { get; set; } = 0;

        public bool Shield = false;
        

        public Consumable Potion = new Consumable("Health Potion", 50, 3);

       
        

        // Constructor
        public Warrior(string name = "Warrior",
            double health = 0,
            double baseAttack = 0,
            double baseBlock = 0)
        {
            Name = name;
            Health = health;
            BaseAttack = baseAttack;
            BaseBlock = baseBlock;
            
        }

        
    }
}
