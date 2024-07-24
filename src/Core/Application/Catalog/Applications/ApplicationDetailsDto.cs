using FSH.WebApi.Application.Catalog.JobPostings;
using FSH.WebApi.Application.Identity.Users;

namespace FSH.WebApi.Application.Catalog.Applications;

public class ApplicationDetailsDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public JobPostingDto JobPosting { get; set; } = default!;
    public UserDetailsDto User { get; set; } = default!;
}