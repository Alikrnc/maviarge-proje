namespace FSH.WebApi.Application.Catalog.Applications;

public class UpdateApplicationRequestValidator : CustomValidator<UpdateApplicationRequest>
{
    public UpdateApplicationRequestValidator(IReadRepository<FSH.WebApi.Domain.Catalog.Application> applicationRepo, IReadRepository<JobPosting> jobpostingRepo, IStringLocalizer<UpdateApplicationRequestValidator> T)
    {

        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (application, name, ct) =>
                    await applicationRepo.FirstOrDefaultAsync(new ApplicationByNameSpec(name), ct)
                        is not FSH.WebApi.Domain.Catalog.Application existingApplication || existingApplication.Id == application.Id)
                .WithMessage((_, name) => T["Application {0} already Exists.", name]);

        RuleFor(p => p.JobPostingId)
            .NotEmpty()
            .MustAsync(async (id, ct) => await jobpostingRepo.GetByIdAsync(id, ct) is not null)
                .WithMessage((_, id) => T["Job Posting {0} Not Found.", id]);
    }
}