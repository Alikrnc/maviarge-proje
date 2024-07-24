using FSH.WebApi.Application.Catalog.JobPostings;

namespace FSH.WebApi.Host.Controllers.Catalog;

public class JobPostingsController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.JobPostings)]
    [OpenApiOperation("Search job postings using available filters.", "")]
    public Task<PaginationResponse<JobPostingDto>> SearchAsync(SearchJobPostingsRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.JobPostings)]
    [OpenApiOperation("Get job posting details.", "")]
    public Task<JobPostingDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetJobPostingRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.JobPostings)]
    [OpenApiOperation("Create a new job posting.", "")]
    public Task<Guid> CreateAsync(CreateJobPostingRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.JobPostings)]
    [OpenApiOperation("Update a job posting.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateJobPostingRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.JobPostings)]
    [OpenApiOperation("Delete a job posting.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteJobPostingRequest(id));
    }

    [HttpPost("generate-random")]
    [MustHavePermission(FSHAction.Generate, FSHResource.JobPostings)]
    [OpenApiOperation("Generate a number of random job postings.", "")]
    public Task<string> GenerateRandomAsync(GenerateRandomJobPostingRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpDelete("delete-random")]
    [MustHavePermission(FSHAction.Clean, FSHResource.JobPostings)]
    [OpenApiOperation("Delete the job postings generated with the generate-random call.", "")]
    [ApiConventionMethod(typeof(FSHApiConventions), nameof(FSHApiConventions.Search))]
    public Task<string> DeleteRandomAsync()
    {
        return Mediator.Send(new DeleteRandomJobPostingRequest());
    }
}