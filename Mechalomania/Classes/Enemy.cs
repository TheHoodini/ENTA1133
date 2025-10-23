using GD14_1133_A1_JuanDiego_DiceGame.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD14_1133_A1_JuanDiego_DiceGame.Classes
{
    public class Enemy
    {
        public string Name { get; set; }
        public string HPName { get; set; }
        private int _hp;
        public int HP
        {
            get => _hp;
            set => _hp = Math.Clamp(value, 0, MaxHP);
        }
        public int MaxHP { get; set; }
        public int Speed { get; set; }
        public string Description { get; set; }
        public List<string> RollAttacks { get; set; }
        public int AttackDamage { get; set; }
        public string Sprite { get; set; } 

        public Enemy(string name, string hpName, int hp, int maxHp, int speed, List<string> rollAttacks, int attackDamage, string sprite, string description)
        {
            Name = name;
            HPName = hpName;
            MaxHP = maxHp;
            HP = hp;
            Speed = speed;
            RollAttacks = rollAttacks;
            AttackDamage = attackDamage;
            Sprite = sprite;
            Description = description;
        }

        public void Attack ()
        {
            Random rng = new();
            TextPrinter textPrinter = new() { PrinterType = "1"};

            int attackIndex = rng.Next(RollAttacks.Count);
            string rollAttackSelected = RollAttacks[attackIndex];

            textPrinter.Print($"The {Name} attacks!");

        }
    }
}
