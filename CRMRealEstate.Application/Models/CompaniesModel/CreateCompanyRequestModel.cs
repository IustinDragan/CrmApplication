namespace CRMRealEstate.Application.Models.CompaniesModel;

public class CreateCompanyRequestModel
{
    public string CompanyName { get; set; }
    public string CompanyIdentityNumber { get; set; }
    public string CompanyPhoneNumber { get; set; }
    public DateTime CompanyCreatedAt { get; set; }
}