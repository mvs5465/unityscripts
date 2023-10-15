using UnityEngine;
using System.Collections.Generic;

namespace Bunker
{
    public class Inventory : ScriptableObject
    {
        public List<ItemData> items = new();

        public List<ItemData> GetItems()
        {
            return items;
        }

        // Returns false if inventory full
        public bool AddItem(ItemData item)
        {
            items.Add(item);
            return true;
        }

        public bool HasItem(ItemData item)
        {
            return items.Contains(item);
        }

        public void RemoveItem(ItemData item)
        {
            items.Remove(item);
        }

        public void RemoveFirstItem()
        {
            if (items[0] != null)
            {
                items.Remove(items[0]);
            }
        }
    }
}