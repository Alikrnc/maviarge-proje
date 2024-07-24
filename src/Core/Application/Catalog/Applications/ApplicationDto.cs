namespace FSH.WebApi.Application.Catalog.Applications;

public class ApplicationDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public Guid JobPostingId { get; set; }
    public string JobPostingName { get; set; } = default!;
    public Guid CandidateInfoId { get; set; }
    public string CandidateInfoName { get; set; } = default!;
}