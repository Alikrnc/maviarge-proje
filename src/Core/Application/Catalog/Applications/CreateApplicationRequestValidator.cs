namespace FSH.WebApi.Application.Catalog.Applications;

public class CreateApplicationRequestValidator : CustomValidator<CreateApplicationRequest>
{
    public CreateApplicationRequestValidator(IReadRepository<FSH.WebApi.Domain.Catalog.Application> applicationRepo, IReadRepository<JobPosting> jobpostingRepo, IReadRepository<CandidateInfo> candidateinfoRepo, IStringLocalizer<CreateApplicationRequestValidator> T)
    {
        RuleFor(n => n.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (name, ct) => await applicationRepo.FirstOrDefaultAsync(new ApplicationByNameSpec(name), ct) is null)
                .WithMessage((_, name) => T["Application {0} already Exists.", name]);

        RuleFor(j => j.JobPostingId)
            .NotEmpty()
            .MustAsync(async (id, ct) => await jobpostingRepo.GetByIdAsync(id, ct) is not null)
                .WithMessage((_, id) => T["Job Posting {0} Not Found.", id]);

        RuleFor(ci => ci.CandidateInfoId)
            .NotEmpty()
            .MustAsync(async (id, ct) => await candidateinfoRepo.GetByIdAsync(id, ct) is not null)
                .WithMessage((_, id) => T["Candidate Info {0} Not Found.", id]);
    }
}