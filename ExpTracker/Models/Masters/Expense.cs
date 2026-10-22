using ExpTracker.Custom_Validations;
using ExpTracker.Enum_Values;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpTracker.Models.Masters
{
    public class Expense
    {
        [Required]
        public Guid Id { get; set; }

        [MaxLength(100)]
        public string Title { get; set; } = "";

        [Required]
        [Precision(18, 2)]
        [Range(typeof(decimal), "0.01", "9999999999999999.99")]
        public decimal Amount { get; set; }

        [Required]
        [NotFutureDateAttribute]
        public DateOnly ExpenseDate { get; set; }

        [Required]
        public Guid CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = null!;

        [Required]
        public PaymentMethod PaymentMethod { get; set; }

        [MaxLength(500)]
        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
