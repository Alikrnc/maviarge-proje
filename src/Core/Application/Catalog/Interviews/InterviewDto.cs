namespace FSH.WebApi.Application.Catalog.Interviews;

public class InterviewDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public Guid ApplicationId { get; set; }
    public string ApplicationName { get; set; } = default!;
}