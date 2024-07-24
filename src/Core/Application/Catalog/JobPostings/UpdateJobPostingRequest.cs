namespace FSH.WebApi.Application.Catalog.JobPostings;

public class UpdateJobPostingRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
}

public class UpdateJobPostingRequestValidator : CustomValidator<UpdateJobPostingRequest>
{
    public UpdateJobPostingRequestValidator(IRepository<JobPosting> repository, IStringLocalizer<UpdateJobPostingRequestValidator> T) =>
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (jobposting, name, ct) =>
                    await repository.FirstOrDefaultAsync(new JobPostingByNameSpec(name), ct)
                        is not JobPosting existingJobPosting || existingJobPosting.Id == jobposting.Id)
                .WithMessage((_, name) => T["Job Posting {0} already Exists.", name]);
}

public class UpdateJobPostingRequestHandler : IRequestHandler<UpdateJobPostingRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<JobPosting> _repository;
    private readonly IStringLocalizer _t;

    public UpdateJobPostingRequestHandler(IRepositoryWithEvents<JobPosting> repository, IStringLocalizer<UpdateJobPostingRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<Guid> Handle(UpdateJobPostingRequest request, CancellationToken cancellationToken)
    {
        var jobposting = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = jobposting
        ?? throw new NotFoundException(_t["Job Posting {0} Not Found.", request.Id]);

        jobposting.Update(request.Name, request.Description);

        await _repository.UpdateAsync(jobposting, cancellationToken);

        return request.Id;
    }
}