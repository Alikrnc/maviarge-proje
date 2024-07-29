namespace FSH.WebApi.Application.Catalog.Applications;

public class ApplicantsBySearchRequestWithJobPostingsSpec : EntitiesByPaginationFilterSpec<FSH.WebApi.Domain.Catalog.Application, ApplicantDto>
{
    public ApplicantsBySearchRequestWithJobPostingsSpec(SearchApplicantsRequest request)
        : base(request) =>
        Query
            .Include(j => j.JobPosting)
            .OrderBy(n => n.Name, !request.HasOrderBy())
            .Where(j => j.JobPostingId.Equals(request.JobPostingId!.Value), request.JobPostingId.HasValue);
}