using CRMRealEstate.Application.Models.AnnouncementModels;
using CRMRealEstate.Application.Models.PropertyModels;

namespace CRMRealEstate.Application.Services.Interfaces
{
    public interface IPropertyService
    {
        Task<IEnumerable<PropertyResponseModel>> GetPropertiesAsync(ReadPropertyRequestModel requestModel);
        Task<PropertyResponseModel> CreatePropertyAsync(CreatePropertyRequestModel createPropertyRequestModel);
        Task<PropertyResponseModel?> GetPropertyByIdAsync(int id);
        Task<PropertyResponseModel> UpdatePropertyAsync(int id, UpdatePropertyRequestModel updateUsersRequestModel);
        Task DeletePropertyAsync(int id);
    }
}
