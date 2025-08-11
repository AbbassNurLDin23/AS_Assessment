using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AS_Assessment.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(100)]
        public string? FullName { get; set; }

        public ICollection<InventoryItem> InventoryItems { get; set; }
    }
}
