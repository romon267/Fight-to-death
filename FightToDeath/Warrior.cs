﻿using System;
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
        public double Gold { get; set; } = 0;
        public Weapon Weapon = new Weapon();

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
         // Setting weapon and equiping
        public void SetWeapon(Weapon wep)
        {
            Weapon.Name = wep.Name;
            Weapon.Type = wep.Type;
            Weapon.Damage = wep.Damage;
            Weapon.Block = wep.Block;
        }

        public void GetWeapon()
        {
            Console.WriteLine($"{Name} has {Weapon.Name}, it is {Weapon.Type}");
        }

        public void EquipWeapon()
        {
            BaseAttack += Weapon.Damage;
            BaseBlock += Weapon.Block;
        }

        // Unique properties, like raise shield, pray, rolling chances etc
        public bool RaiseShield = false;
        public bool SharpDaggers = false;


    }
}
