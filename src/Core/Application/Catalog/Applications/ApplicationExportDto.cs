namespace FSH.WebApi.Application.Catalog.Applications;

public class ApplicationExportDto : IDto
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string JobPostingName { get; set; } = default!;
    public string CandidateInfoName { get; set; } = default!;
}