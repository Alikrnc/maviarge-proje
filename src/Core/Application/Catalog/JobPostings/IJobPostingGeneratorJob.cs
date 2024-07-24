using System.ComponentModel;

namespace FSH.WebApi.Application.Catalog.JobPostings;

public interface IJobPostingGeneratorJob : IScopedService
{
    [DisplayName("Generate Random job posting example job on Queue notDefault")]
    Task GenerateAsync(int nSeed, CancellationToken cancellationToken);

    [DisplayName("removes all random job postings created example job on Queue notDefault")]
    Task CleanAsync(CancellationToken cancellationToken);
}
