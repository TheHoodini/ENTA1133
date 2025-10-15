using GD14_1133_A1_JuanDiego_DiceGame.Combat;
using GD14_1133_A1_JuanDiego_DiceGame.Tools;
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
        public string EffectRoll { get; set; } 
        public string Description { get; set; }
        public Item(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public abstract void Use();

        public virtual void Info()
        {
            TextPrinter printer = new()
            {
                PrinterType = "1"
            };
            printer.Print($"{Name}: {Description}");
        }
    }

    // -------------------------------------- Combat --------------------------------------
    public abstract class ItemCombat : Item
    {
        public ItemCombat(string name, string effectRoll, string description) : base(name, description) 
        {
            EffectRoll = effectRoll;
        }
    }

    // Weapon item
    public class ItemWeapon : ItemCombat
    {
        public int Damage { get; set; }

        public ItemWeapon(string name, int damage, string effectRoll, string description) : base(name, effectRoll, description)
        {
            Damage = damage;
        }

        public override void Use()
        {
            
        }
        public override void Info()
        {
            TextPrinter printer = new()
            {
                PrinterType = "1"
            };
            string weaponDescription = $"{Description}\nDamage: {Damage} + {EffectRoll}. ";
            printer.Print($"{Name}: {weaponDescription}");
        }

        public int Use(Player player, Enemy enemy)
        {
            TextPrinter printer = new()
            {
                PrinterType = "1"
            };
            printer.Print($"You used the {Name}.");
            int totalDamage = CombatRoller.Attack(Damage, EffectRoll, true);
            return totalDamage;
        }

    }

    // Consumable item
    public class ItemConsumable : ItemCombat
    {
        public string Effect { get; set; }

        public ItemConsumable(string name, string effect, string effectRoll, string description) : base(name, effectRoll, description)
        {
            Effect = effect;
        }

        public override void Use()
        {
            
        }

        public override void Info()
        {
            TextPrinter printer = new()
            {
                PrinterType = "1"
            };
            string consumableDescription = $"{Description}\nEffect: {EffectRoll} {Effect}.";
            printer.Print($"{Name}: {consumableDescription}");
        }

        public void Use(Player player)
        {
            TextPrinter printer = new()
            {
                PrinterType = "1"
            };
            printer.Print($"You used the {Name}.");
            if (Effect == "heal")
            {
                player.HP += CombatRoller.Heal(EffectRoll);
            }
        }

    }

    // -------------------------------------- Loot --------------------------------------
    public abstract class ItemLoot : Item
    {
        public ItemLoot(string name, string description) : base(name, description) { }
    }

    // Key
    public class ItemKey : ItemLoot
    {

        public ItemKey(string name, string description) : base(name, description)
        {

        }

        public override void Use()
        {
            Console.WriteLine("You used the key");
        }

    }

    // Sellable item
    public class ItemSellable : ItemLoot
    {
        public int Price { get; set; }

        public ItemSellable(string name, int price, string description) : base(name, description)
        {
            Price = price;
        }

        public override void Use()
        {
            Console.WriteLine($"{Name} can be sold for {Price} coins.");
        }

    }

    public class ItemUnknown : Item
    {
        public ItemUnknown() : base("Unknown", "") { }

        public override void Use()
        {
            Console.WriteLine("You can't use this");
        }

    }
}
