using System;
using System.Collections.Generic;
using System.IO;

namespace Project.Components.Pages.Codes
{
    internal class TableSlot : AbstractTableSlot
    {
        // Fields
        string customerFirstName;
        string customerLastName;
        int tableNumber;
        int startTime;
        bool isAvailable;
        int endTime;

        // Properties
        public override string CustomerFirstName { get { return customerFirstName; } }
        public override string CustomerLastName { get { return customerLastName; } }
        public override int TableNumber { get { return tableNumber; } }
        public override int StartTime { get { return startTime; } }
        public override bool IsAvailable { get { return isAvailable; } }
        public override int EndTime { get { return endTime; } }

        // List to hold table slots
        List<TableSlot> tableSlots = new List<TableSlot>();

        // Property to expose table slots
        public List<TableSlot> TableSlots { get { return tableSlots; } }

        // Path to the tables file
        public const string TABLES_TXT = "C:\\Tables.csv";

        // Method to load table slots from file
        public override void LoadSlots()
        {
            try
            {
                string[] lines = File.ReadAllLines(TABLES_TXT);

                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');

                    int tableNumber = int.Parse(parts[0]);
                    int startHour = int.Parse(parts[1]);
                    int availability = int.Parse(parts[2]);
                    bool isAvailable = availability == 0 ? true : false;

                    tableSlots.Add(new TableSlot(tableNumber, startHour, startHour + 1, isAvailable));
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"File {TABLES_TXT} not found: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while loading table slots: {ex.Message}");
            }
        }

        // Constructor to load slots when an instance is created
        public TableSlot()
        {
            LoadSlots();
        }

        // Constructor with parameters
        public TableSlot(int _tableNumber, int _startTime, int _endTime, bool _isAvailable, string _customerFirstName = "", string _customerLastName = "")
        {
            tableNumber = _tableNumber;
            startTime = _startTime;
            isAvailable = _isAvailable;
            endTime = _endTime;
            customerFirstName = _customerFirstName;
            customerLastName = _customerLastName;
        }

        // Method to make a reservation
        public override TableSlot MakeReservation(string customerFirstName, string customerLastName, int tableNumber, int startTime)
        {
            try
            {
                foreach (TableSlot slot in tableSlots)
                {
                    if (slot.TableNumber == tableNumber && slot.StartTime == startTime && slot.IsAvailable)
                    {
                        slot.isAvailable = false;
                        slot.customerFirstName = customerFirstName;
                        slot.customerLastName = customerLastName;
                        Save();
                        return slot;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while making reservation: {ex.Message}");
            }

            return null;
        }

        // Method to cancel a reservation
        public override void CancelReservation(int tableNumber, int startTime)
        {
            try
            {
                foreach (TableSlot slot in tableSlots)
                {
                    if (slot.TableNumber == tableNumber && slot.StartTime == startTime)
                    {
                        slot.isAvailable = true;
                    }
                }

                Save();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while canceling reservation: {ex.Message}");
            }
        }

        // Method to save changes
        public override void Save()
        {
            try
            {
                List<string> lines = new List<string>();

                foreach (var slot in tableSlots)
                {
                    int availability = slot.IsAvailable ? 0 : 1;
                    string line = $"{slot.TableNumber},{slot.StartTime},{availability},{slot.CustomerFirstName},{slot.CustomerLastName}";
                    lines.Add(line);
                }

                File.WriteAllLines(TABLES_TXT, lines);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while saving table slots: {ex.Message}");
            }
        }
    }
}
