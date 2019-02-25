using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightToDeath 
{
    class Weapon : Item
    {
        public string Type { get; set; } = "None";
        public double Damage { get; set; } = 0;
        public double Block { get; set; } = 0;

        public Weapon()
        {
            Name = "No name";
            Type = "None";
            Damage = 0;
            Block = 0;
        }

        public Weapon(string name, string type, double damage, double block)
        {
            Name = name;
            Type = type;
            Damage = damage;
            Block = block;
        }

        
    }
}
