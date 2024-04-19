using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Components.Pages.Codes
{
    internal abstract class AbstractTableSlot
    {
        // Properties to store values for booking table
        public abstract string CustomerFirstName { get; }
        public abstract string CustomerLastName { get; }
        public abstract int TableNumber { get; }
        public abstract int StartTime { get; }
        public abstract bool IsAvailable { get; }
        public abstract int EndTime { get; }

        // Method to Load table slots data
        public abstract void LoadSlots();

        // Method to make reservation for the table slot
        public abstract AbstractTableSlot MakeReservation(string customerFirstName, string customerLastName, int tableNumber, int startTime);

        // Method to cancel a reservation for the table slot
        public abstract void CancelReservation(int tableNumber, int startTime);

        // Method to save changes made to the table slots in the data file
        public abstract void Save();  
    }
}

