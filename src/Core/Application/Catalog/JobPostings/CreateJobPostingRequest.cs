namespace FSH.WebApi.Application.Catalog.JobPostings;

public class CreateJobPostingRequest : IRequest<Guid>
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
}

public class CreateJobPostingRequestValidator : CustomValidator<CreateJobPostingRequest>
{
    public CreateJobPostingRequestValidator(IReadRepository<JobPosting> repository, IStringLocalizer<CreateJobPostingRequestValidator> T) =>
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (name, ct) => await repository.FirstOrDefaultAsync(new JobPostingByNameSpec(name), ct) is null)
                .WithMessage((_, name) => T["Job Posting {0} already Exists.", name]);
}

public class CreateJobPostingRequestHandler : IRequestHandler<CreateJobPostingRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<JobPosting> _repository;

    public CreateJobPostingRequestHandler(IRepositoryWithEvents<JobPosting> repository) => _repository = repository;

    public async Task<Guid> Handle(CreateJobPostingRequest request, CancellationToken cancellationToken)
    {
        var jobposting = new JobPosting(request.Name, request.Description);

        await _repository.AddAsync(jobposting, cancellationToken);

        return jobposting.Id;
    }
}