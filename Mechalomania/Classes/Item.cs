using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD14_1133_A1_JuanDiego_DiceGame.Classes
{
    /*
    Classes structure:
    
    Item (abstract)
    ├── Combat (abstract)
    │   ├── Weapon
    │   └── Consumable
    └── Loot (abstract)
        ├── Key
        └── Sellable 
     */
    public abstract class Item
    {
        public string Name { get; set; }

        public Item(string name)
        {
            Name = name;
        }

        public abstract void Use();
        public abstract string Info();
    }

    // -------------------------------------- Combat --------------------------------------
    public abstract class Combat : Item
    {
        public Combat(string name) : base(name) { }
    }

    
    public class Weapon : Combat
    {
        public int Damage { get; set; }

        public Weapon(string name, int damage) : base(name)
        {
            Damage = damage;
        }

        public override void Use()
        {
            Console.WriteLine($"{Name} used to deal {Damage} damage!");
        }

        public override string Info()
        {
            return $"{Name} (Weapon) - Deals {Damage} damage.";
        }
    }

    
    public class Consumable : Combat
    {
        public string Effect { get; set; }

        public Consumable(string name, string effect) : base(name)
        {
            Effect = effect;
        }

        public override void Use()
        {
            Console.WriteLine($"{Name} consumed: {Effect}");
        }

        public override string Info()
        {
            return $"{Name} - Effect: {Effect}";
        }
    }

    // -------------------------------------- Loot --------------------------------------
    public abstract class Loot : Item
    {
        public Loot(string name) : base(name) { }
    }

    
    public class Key : Loot
    {
        public string Unlocks { get; set; }

        public Key(string name, string unlocks) : base(name)
        {
            Unlocks = unlocks;
        }

        public override void Use()
        {
            Console.WriteLine($"{Name} used to unlock {Unlocks}.");
        }

        public override string Info()
        {
            return $"{Name} (Key) - Unlocks: {Unlocks}";
        }
    }

    public class Sellable : Loot
    {
        public int Price { get; set; }

        public Sellable(string name, int price) : base(name)
        {
            Price = price;
        }

        public override void Use()
        {
            Console.WriteLine($"{Name} can be sold for {Price} coins.");
        }

        public override string Info()
        {
            return $"{Name} - Worth {Price} coins.";
        }
    }
}
