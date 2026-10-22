using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpTracker.Models.Masters
{
    public class Budget
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = null!;

        [Required]
        public int Year { get; set; }

        [Range(1, 12)]
        public int Month { get; set; }

        [Precision(18, 2)]
        [Range(typeof(decimal), "0.01", "9999999999999999.99")]
        public decimal LimitAmount { get; set; }
    }
}
