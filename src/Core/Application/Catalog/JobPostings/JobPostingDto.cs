namespace FSH.WebApi.Application.Catalog.JobPostings;

public class JobPostingDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
}