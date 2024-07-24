using FSH.WebApi.Application.Catalog.Applications;

namespace FSH.WebApi.Application.Catalog.Interviews;

public class InterviewDetailsDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public ApplicationDto Application { get; set; } = default!;
}