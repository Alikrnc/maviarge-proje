namespace FSH.WebApi.Application.Catalog.Applications;

public class ApplicationsByJobPostingSpec : Specification<FSH.WebApi.Domain.Catalog.Application>
{
    public ApplicationsByJobPostingSpec(Guid? jobpostingId)
    {
        if (jobpostingId.HasValue)
        {
            Query.Where(j => j.JobPostingId == jobpostingId.Value);
        }
    }
}
