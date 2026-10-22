using ExpTracker.Dtos.Masters;

namespace ExpTracker.Interfaces.Masters
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> GetAllAsync();
        Task<CategoryDto?> GetByIdAsync(Guid id);
    }
}
