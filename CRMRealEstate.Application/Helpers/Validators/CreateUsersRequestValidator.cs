using CRMRealEstate.Application.Models.CompaniesModel;
using CRMRealEstate.Application.Models.UsersModels;
using FluentValidation;

namespace CRMRealEstate.Application.Helpers.Validators;

public class CreateUsersRequestValidator : AbstractValidator<CreateUsersRequestModel>
{
    public CreateUsersRequestValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().Length(1, 50);
        RuleFor(x => x.LastName).NotEmpty().Length(1, 50);
        RuleFor(x => x.UserName).NotEmpty().Length(1, 50);
        RuleFor(x => x.Email).NotEmpty().Length(1, 50).EmailAddress();
        RuleFor(x => x.Password).NotEmpty().Length(1, 50);
        RuleFor(x => x.PhoneNumber).NotEmpty().Length(1, 50);

        if (new CreateUsersRequestModel().isAgent != false)
            RuleFor(x => x.Company).NotNull().When(x => x.isAgent).WithMessage("The Company field is required for agents.");

       // RuleFor(x => x.Company).NotNull().SetValidator(new CompanyValidator());


    }
}

public class CompanyValidator : AbstractValidator<CreateCompanyRequestModel>
{
    public CompanyValidator()
    {
        RuleFor(x => x.CompanyName).NotEmpty().Length(1, 50);
        RuleFor(x => x.CompanyIdentityNumber).NotEmpty().Length(3, 10);
        RuleFor(x => x.CompanyPhoneNumber).NotEmpty().Length(10);
    }
}