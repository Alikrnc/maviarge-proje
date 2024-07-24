namespace FSH.WebApi.Application.Catalog.Interviews;

public class InterviewExportDto : IDto
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string ApplicationName { get; set; } = default!;
}