using System.ComponentModel.DataAnnotations;
using CRMRealEstate.Application.Models.CompaniesModel;
using CRMRealEstate.DataAccess.Entities;

namespace CRMRealEstate.Application.Models.UsersModels;

public class CreateUsersRequestModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    [EmailAddress] public string Email { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public bool isAgent { get; set; }
    public CreateCompanyRequestModel Company { get; set; }

    public Users ToUser()
    {
        return new Users
        {
            FirstName = FirstName,
            LastName = LastName,
            UserName = UserName,
            Email = Email,
            Password = Password,
            PhoneNumber = PhoneNumber,
            Company = !isAgent
                ? null
                : new Company
                {
                    CompanyName = Company.CompanyName,
                    CompanyIdentityNumber = Company.CompanyIdentityNumber,
                    CompanyPhoneNumber = Company.CompanyPhoneNumber,
                    CompanyCreatedAt = Company.CompanyCreatedAt
                }
        };
    }
}