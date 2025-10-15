using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD14_1133_A1_JuanDiego_DiceGame.Classes
{
    public static class ItemList
    {
        private static Dictionary<string, Item> _items = new Dictionary<string, Item>();
        public enum ItemCategory
        {
            Combat,
            Loot,
            Any
        }

        static ItemList()
        {
            AddToList(new ItemWeapon("Hammer", 5, "1d10", "A hammer commonly used by workers.\nDamage: 5 + 1d10"));
            AddToList(new ItemWeapon("Wrench", 2, "2d4", "The number 1 tool to repair pipes.\nDamage: 2 + 2d4"));
            AddToList(new ItemWeapon("Screwdriver", 1, "2d3", "The simplest of tools.\nDamage: 1 + 2d3"));

            AddToList(new ItemConsumable("Canteen", "heal", "1d20", "A bottle filled with clean water.\nHeal: 1d20"));
            AddToList(new ItemConsumable("Coffee", "heal", "2d10", "A good drink for sleepy times.\n Heal: 2d10"));

            AddToList(new ItemKey("Rusted Key", "An old but sturdy key that seems to work.\nEffect: Can open locked doors if you <check> them"));
            AddToList(new ItemKey("Crowbar", "One of The Mechanic's tools that anyone can use!\nEffect: Can avoid traps if you <check> them"));

            AddToList(new ItemSellable("Silver Watch", 15, "Somehow it still works.\nValue: $15"));
            AddToList(new ItemSellable("Ring", 25, "A shiny ring with a pearl on top.\nValue: $25"));

            // Default item for errors
            AddToList(new ItemUnknown());
        }

        private static void AddToList(Item item)
        {
            _items[item.Name.ToLower()] = item; 
        }

        public static Item Get(string name)
        {
            if (_items.TryGetValue(name, out var item))
                return item;

            return _items["unknown"];
        }

        public static Dictionary<string, int> GetRandomItems(ItemCategory category, int amount)
        {
            var itemSelection = _items.Values.Where(item =>
                category == ItemCategory.Any ||
                (category == ItemCategory.Combat && (item is ItemWeapon || item is ItemConsumable)) ||
                (category == ItemCategory.Loot && (item is ItemKey || item is ItemSellable))
            ).ToList();

            //if (itemSelection.Count == 0) throw new Exception($"No items found for '{category}'");

            var rng = new Random();
            var result = new Dictionary<string, int>();

            // Give random items
            for (int i = 0; i < amount; i++)
            {
                var randomItem = itemSelection[rng.Next(itemSelection.Count)];
                var itemName = randomItem.Name.ToLower();

                if (result.ContainsKey(itemName))
                    result[itemName]++;
                else
                    result[itemName] = 1;
            }

            return result;
        }






    }

}
