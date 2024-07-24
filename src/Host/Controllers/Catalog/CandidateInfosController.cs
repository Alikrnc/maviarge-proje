using FSH.WebApi.Application.Catalog.CandidateInfos;

namespace FSH.WebApi.Host.Controllers.Catalog;

public class CandidateInfosController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.CandidateInfos)]
    [OpenApiOperation("Search candidate informations using available filters.", "")]
    public Task<PaginationResponse<CandidateInfoDto>> SearchAsync(SearchCandidateInfosRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.CandidateInfos)]
    [OpenApiOperation("Get candidate informations details.", "")]
    public Task<CandidateInfoDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetCandidateInfoRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.CandidateInfos)]
    [OpenApiOperation("Create a new candidate informations.", "")]
    public Task<Guid> CreateAsync(CreateCandidateInfoRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.CandidateInfos)]
    [OpenApiOperation("Update a candidate informations.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateCandidateInfoRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.CandidateInfos)]
    [OpenApiOperation("Delete a candidate informations.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteCandidateInfoRequest(id));
    }

    [HttpPost("generate-random")]
    [MustHavePermission(FSHAction.Generate, FSHResource.CandidateInfos)]
    [OpenApiOperation("Generate a number of random candidate informations.", "")]
    public Task<string> GenerateRandomAsync(GenerateRandomCandidateInfoRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpDelete("delete-random")]
    [MustHavePermission(FSHAction.Clean, FSHResource.CandidateInfos)]
    [OpenApiOperation("Delete the candidate informations generated with the generate-random call.", "")]
    [ApiConventionMethod(typeof(FSHApiConventions), nameof(FSHApiConventions.Search))]
    public Task<string> DeleteRandomAsync()
    {
        return Mediator.Send(new DeleteRandomCandidateInfoRequest());
    }
}