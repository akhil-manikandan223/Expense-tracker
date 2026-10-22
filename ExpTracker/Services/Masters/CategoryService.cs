using ExpTracker.Data;
using ExpTracker.Dtos.Masters;
using ExpTracker.Interfaces.Masters;
using Microsoft.EntityFrameworkCore;

namespace ExpTracker.Services.Masters
{
    public class CategoryService(AppDbContext db) : ICategoryService
    {
        public async Task<List<CategoryDto>> GetAllAsync()
        {
            return await db.Categories
                .AsNoTracking()
                .Select(e => new CategoryDto(e.Id, e.Name, e.Description, e.IsActive, e.CreatedAt))
                .ToListAsync();
        }

        public async Task<CategoryDto?> GetByIdAsync(Guid id)
        {
            return await db.Categories
                .AsNoTracking()
                .Where(e => e.Id == id)
                .Select(e => new CategoryDto(e.Id, e.Name, e.Description, e.IsActive, e.CreatedAt))
                .FirstOrDefaultAsync();
        }
    }
}
