namespace FSH.WebApi.Application.Catalog.JobPostings;

public class JobPostingByNameSpec : Specification<JobPosting>, ISingleResultSpecification
{
    public JobPostingByNameSpec(string name) =>
        Query.Where(b => b.Name == name);
}