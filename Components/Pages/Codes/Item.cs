using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Project.Components.Pages.Codes
{
    internal class Item : AbstractItem
    {
        // Fields
        int itemNumber;
        string description;
        string type;
        int quantity;
        decimal price;

        // Constants
        public const string ITEMS_FILE = "C:\\Items.csv";

        // List to hold items
        List<Item> items = new List<Item>();

        // Property to expose items list
        public List<Item> Items { get { return items; } }

        // Properties
        public override int ItemNumber { get { return itemNumber; } }
        public override string Description { get { return description; } }
        public override string Type { get { return type; } }
        public override int Quantity { get { return quantity; } }
        public override decimal Price { get { return price; } }

        // Method to load items from file
        public override void LoadItems()
        {
            try
            {
                string[] lines = File.ReadAllLines(ITEMS_FILE);

                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');

                    if (parts.Length < 5)
                    {
                        throw new Exception("Invalid format in items file.");
                    }

                    int itemNumber = int.Parse(parts[0]);
                    string description = parts[1];
                    string type = parts[2];
                    int quantity = int.Parse(parts[3]);
                    decimal price = decimal.Parse(parts[4]);

                    items.Add(new Item(itemNumber, description, type, quantity, price));
                }
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException($"The file '{ITEMS_FILE}' was not found.");
            }
            catch (DirectoryNotFoundException)
            {
                throw new DirectoryNotFoundException($"The directory for '{ITEMS_FILE}' was not found.");
            }
            catch (FormatException)
            {
                throw new FormatException($"Invalid format in '{ITEMS_FILE}'.");
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while loading items: {ex.Message}");
            }
        }

        // Constructor to load items when an instance is created
        public Item()
        {
            LoadItems();
        }

        // Constructor with parameters
        public Item(int _itemNumber, string _description, string _type, int _quantity, decimal _price)
        {
            itemNumber = _itemNumber;
            description = _description;
            type = _type;
            quantity = _quantity;
            price = _price;
        }

        // Method to order an item
        public override Item OrderItem(string _item, int _quantity)
        {
            foreach (Item item in items)
            {
                if (item.Description == _item && (item.quantity - _quantity) >= 0)
                {
                    item.quantity -= _quantity;
                    SaveItems();
                    decimal totalPrice = _quantity * item.price;
                    Item orderItem = new Item(item.itemNumber, item.description, item.type, _quantity, totalPrice);

                    return orderItem;
                }
            }

            return null;
        }

        // Method to save items to file
        public override void SaveItems()
        {
            StringBuilder content = new StringBuilder();

            foreach (var item in items)
            {
                content.AppendLine($"{item.ItemNumber},{item.Description},{item.Type},{item.Quantity},{item.Price}");
            }

            File.WriteAllText(ITEMS_FILE, content.ToString());

        }

        // Method to add an item
        public override Item AddItem(string _item, int _quantity)
        {
            foreach (Item item in items)
            {
                if (item.Description == _item)
                {
                    item.quantity += _quantity;
                    SaveItems();
                    return item;
                }
            }

            return null;

        }

        // Method to generate a unique bill number
        public override int GenerateUniqueBillNumber()
        {
            return (int)(DateTime.UtcNow.Subtract(new DateTime(2022, 1, 1))).TotalSeconds;
        }
    }
}
