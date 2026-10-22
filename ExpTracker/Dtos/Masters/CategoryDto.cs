namespace ExpTracker.Dtos.Masters
{
    public record CategoryDto(Guid Id, string Name, string? Description, bool IsActive, DateTime CreatedAt);
}
