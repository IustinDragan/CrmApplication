using CRMRealEstate.Application.Models.AdressModels;
using FluentValidation;

namespace CRMRealEstate.Application.Validators
{
    public class AddressRequestModelValidator : AbstractValidator<CreateAdressRequestModel>
    {
        public AddressRequestModelValidator()
        {
            RuleFor(a => a.Street)
                .NotEmpty().WithMessage("{PropertyName} shouldn't be empty");
            RuleFor(a => a.StreetNumber)
                .NotEmpty().WithMessage("{PropertyName} shouldn't be empty");
            RuleFor(a => a.City)
                .NotEmpty().WithMessage("{PropertyName} shouldn't be empty");
        }
    }
}
