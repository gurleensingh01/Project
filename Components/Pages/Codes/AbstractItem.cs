using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Project.Components.Pages.Codes
{
    internal abstract class AbstractItem
    {
        // Properties
        public abstract int ItemNumber { get; }
        public abstract string Description { get; }
        public abstract string Type { get; }
        public abstract int Quantity { get; }
        public abstract decimal Price { get; }

        // Method to load items data from the file
        public abstract void LoadItems();

        // Method to order an item
        public abstract AbstractItem OrderItem(string _item, int _quantity);

        // Method to save changes made to the items in the file
        public abstract void SaveItems();

        // Method to add an item to the inventory
        public abstract AbstractItem AddItem(string _item, int _quantity);

        // Method to generate a unique bill number
        public abstract int GenerateUniqueBillNumber();  
    }
}
