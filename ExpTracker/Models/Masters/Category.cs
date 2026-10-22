using System.ComponentModel.DataAnnotations;

namespace ExpTracker.Models.Masters
{
    public class Category
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = "";

        [MaxLength(200)]
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
