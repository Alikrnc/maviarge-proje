namespace FSH.WebApi.Application.Identity.Users;

public class UploadPhotoRequestValidator : CustomValidator<UploadPhotoRequest>
{
    public UploadPhotoRequestValidator(IUserService userService)
    {
        RuleFor(p => p.PhotoPath);
        RuleFor(p => p.Photo);
    }
}