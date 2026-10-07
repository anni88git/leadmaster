using FluentValidation;
using LeadManager.DTOs;

namespace LeadManager.Validation;

public class CreateLeadValidator : AbstractValidator<CreateLeadDto>
{
    public CreateLeadValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("A valid email is required.");
    }
}

public class UpdateLeadValidator : AbstractValidator<UpdateLeadDto>
{
    public UpdateLeadValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("A valid email is required.");
    }
}
