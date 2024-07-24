using System.ComponentModel;

namespace FSH.WebApi.Application.Catalog.CandidateInfos;

public interface ICandidateInfoGeneratorJob : IScopedService
{
    [DisplayName("Generate Random candidate information example job on Queue notDefault")]
    Task GenerateAsync(int nSeed, CancellationToken cancellationToken);

    [DisplayName("removes all random candidate informations created example job on Queue notDefault")]
    Task CleanAsync(CancellationToken cancellationToken);
}
