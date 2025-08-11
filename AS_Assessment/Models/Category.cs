namespace AS_Assessment.Models
{
    public class Category
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }

        public bool IsDeleted { get; set; } = false;
        public ICollection<InventoryItem> InventoryItems { get; set; } = new List<InventoryItem>();
    }
    
}
