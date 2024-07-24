namespace FSH.WebApi.Application.Catalog.CandidateInfos;

public class CandidateInfoByNameSpec : Specification<CandidateInfo>, ISingleResultSpecification
{
    public CandidateInfoByNameSpec(string name) =>
        Query.Where(b => b.Name == name);
}