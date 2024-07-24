namespace FSH.WebApi.Application.Catalog.Applications;

public class ApplicationByIdWithJobPostingSpec : Specification<FSH.WebApi.Domain.Catalog.Application, ApplicationDetailsDto>, ISingleResultSpecification
{
    public ApplicationByIdWithJobPostingSpec(Guid id)
    {
            Query
                .Where(j => j.Id == id)
                .Include(j => j.JobPosting);

            Query
                .Where(ci => ci.Id == id)
                .Include(ci => ci.CandidateInfo);
    }
}
