using FluentValidation;

namespace CatalogService.ApiService.Products.Models;

public class NewProductDtoValidator : AbstractValidator<NewProductDto>
{
    public NewProductDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(200);

        const int oneMb = 1024 * 1024;
        const int maxUploadSizeInMb = 4 * oneMb;
        const int maxUploadBase64LengthInMb = maxUploadSizeInMb * 4 / 3;

        RuleFor(x => x.ImageBase64)
            .MaximumLength(maxUploadBase64LengthInMb);
    }
}