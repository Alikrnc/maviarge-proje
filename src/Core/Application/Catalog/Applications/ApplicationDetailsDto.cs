using FSH.WebApi.Application.Catalog.JobPostings;
using FSH.WebApi.Application.Catalog.CandidateInfos;

namespace FSH.WebApi.Application.Catalog.Applications;

public class ApplicationDetailsDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public JobPostingDto JobPosting { get; set; } = default!;
    public CandidateInfoDto CandidateInfo { get; set; } = default!;
}