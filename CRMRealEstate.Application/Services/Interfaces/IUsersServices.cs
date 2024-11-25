using CRMRealEstate.Application.Models.UsersModels;

namespace CRMRealEstate.Application.Services.Interfaces;

public interface IUsersServices
{
    Task<IEnumerable<UsersResponseModel>> RealAllUsersAsync();
    Task<UsersResponseModel?> GetUserByIdAsync(int id, bool includeCompanyDetails);
    Task<UsersResponseModel> GetUserByNameAsync(string userName, bool includeCompanyDetails);
    Task<UsersResponseModel> CreateUserAsync(CreateUsersRequestModel requestModel);
    Task<UsersResponseModel> UpdateUserAsync(int id, CreateUsersRequestModel requestModel);

    Task DeleteUserAsync(int id);

    Task<bool> isEmailUniqueAsync(string email);

    Task<int?> AddAnnouncementToFavoriteAsync(int userId, int announcementId);
}