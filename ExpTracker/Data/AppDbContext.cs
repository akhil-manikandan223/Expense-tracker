using ExpTracker.Models.Masters;
using Microsoft.EntityFrameworkCore;

namespace ExpTracker.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Expense> Expenses => Set<Expense>();
        public DbSet<Budget> Budgets => Set<Budget>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Category
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(50);

                entity.HasIndex(c => c.Name)
                .IsUnique();

                entity.Property(c => c.Description)
                .HasMaxLength(200);

                entity.Property(c => c.IsActive)
                .HasDefaultValue(true);

                entity.Property(c => c.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");
            });
            #endregion

            #region Expense
            modelBuilder.Entity<Expense>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(100);

                entity.Property(e => e.Amount)
                .HasPrecision(18, 2);

                entity.ToTable(t => t.HasCheckConstraint(
                    "CK_Expense_Amount_Positive",
                    "[Amount] > 0"
                    ));

                entity.Property(e => e.Notes)
                    .HasMaxLength(500);

                entity.Property(e => e.PaymentMethod)
                .HasConversion<int>();

                entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

                entity.Property(e => e.UpdatedAt)
                .IsRequired(false);

                entity.HasOne(e => e.Category)
                .WithMany()
                .HasForeignKey(e => e.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
            });
            #endregion

            #region Budget
            modelBuilder.Entity<Budget>(entity =>
            {
                entity.HasKey(b => b.Id);

                entity.Property(b => b.Year)
                .IsRequired();

                entity.Property(b => b.Month)
                    .IsRequired();

                entity.ToTable(t =>
                {
                    t.HasCheckConstraint(
                            "CK_Budgets_Month_Range",
                            "[Month] >= 1 AND [Month] <= 12"
                        );

                    t.HasCheckConstraint(
                            "CK_Budgets_LimitAmount_Positive",
                            "[LimitAmount] > 0"
                        );
                });

                entity.Property(b => b.LimitAmount)
                    .HasPrecision(18, 2);

                entity.HasIndex(b => new
                {
                    b.CategoryId,
                    b.Year,
                    b.Month
                })
                .IsUnique();

                entity.HasOne(b => b.Category)
                    .WithMany()
                    .HasForeignKey(b => b.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
            #endregion
        }
    }
}
