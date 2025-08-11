using AS_Assessment.Enums;

namespace AS_Assessment.Models
{
    public class InventoryItem
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();  // Primary key (useful if storing in a database)

        public string Name { get; set; }  // Name of the item

        public int Quantity { get; set; }  // Current quantity in stock

        public StockStatus StockStatus { get; set; }  // e.g. "In Stock", "Low Stock", "Out of Stock"

        public string WhatNeeded { get; set; }  // Description of what is needed (e.g. "Order more", "None")

        public bool IsDeleted { get; set; } = false;
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string CategoryId { get; set; }
        public Category Category { get; set; }
    }

}
