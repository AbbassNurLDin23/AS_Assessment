using AS_Assessment.Enums;

namespace AS_Assessment.DTOs
{
    public class InventoryItemCreateDto
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public StockStatus StockStatus { get; set; }
        public string WhatNeeded { get; set; }
        public string CategoryId { get; set; }
        public string? UserId { get; set; }
    }

    public class InventoryItemUpdateDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public StockStatus StockStatus { get; set; }
        public string WhatNeeded { get; set; }
        public string CategoryId { get; set; }
        public string UserId { get; set; }
    }

    public class InventoryItemReadDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public StockStatus StockStatus { get; set; }
        public string WhatNeeded { get; set; }
        public string CategoryName { get; set; }
        public string CategoryId { get; set; }      // Add this
        public string UserId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
